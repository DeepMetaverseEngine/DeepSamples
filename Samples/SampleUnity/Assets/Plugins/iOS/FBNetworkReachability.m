#import "FBNetworkReachability.h"
#import <CoreTelephony/CTTelephonyNetworkInfo.h>
#import <CoreTelephony/CTCarrier.h>
#import <arpa/inet.h>
#import <ifaddrs.h>
#import <net/if.h>



@interface FBNetworkReachability()
@property (assign) FBNetworkReachabilityConnectionMode connectionMode;
@property (copy) NSString* ipaddress;
@end


#define NOT_CONNECT 0
#define WIFI       1
#define NETWORK_2G  2
#define NETWORK_3G  3
#define NETWORK_4G  4


@implementation FBNetworkReachability



@synthesize connectionMode = connectionMode_;
@synthesize ipaddress;

//------------------------------------------------------------------------------
#pragma mark -
#pragma mark Initialization and deallocation
//------------------------------------------------------------------------------
- (id)init
{
    self = [super init];
	if (self) {
        struct sockaddr_in	sockaddr;
        bzero(&sockaddr, sizeof(sockaddr));
        sockaddr.sin_len = sizeof(sockaddr);
        sockaddr.sin_family = AF_INET;
        inet_aton("0.0.0.0", &sockaddr.sin_addr);        

        reachability_ =
            SCNetworkReachabilityCreateWithAddress(NULL, (struct sockaddr *) &sockaddr);

		self.connectionMode = FBNetworkReachableUninitialization;
        
        [self refresh];
        [self startNotifier];
	}
   
	return self;
}

- (void) dealloc
{
    [self stopNotifier];
	CFRelease(reachability_);
	[super dealloc];
}


//------------------------------------------------------------------------------
#pragma mark -
#pragma mark Private
//------------------------------------------------------------------------------
- (NSString*)_getIPAddressWiFilEnabled:(BOOL)wifiEnabled
{
    // priority:
    // (1) WiFi "en0", "en1", ...
    // (2) WWAN "pdp_ip0", ...
    //
    // memo:
    // name=pdp_ip0
    // addr=126.202.8.39

	struct ifaddrs * addrs;
	const struct ifaddrs * cursor;
    
    NSString* addressStringForWiFi = nil;
    NSString* addressStringForWWAN = nil;
	
	if (getifaddrs(&addrs) == 0) {
		cursor = addrs;
		while (cursor != NULL) {
			if (cursor->ifa_addr->sa_family == AF_INET
				&& (cursor->ifa_flags & IFF_LOOPBACK) == 0) {
				NSString *name =
				[NSString stringWithUTF8String:cursor->ifa_name];
				
                // found the WiFi adapter
				if ([name isEqualToString:@"en0"] ||	// iPhone
					[name isEqualToString:@"en1"]) {	// Simulator (Mac)
					addressStringForWiFi = [NSString stringWithUTF8String:
                                            inet_ntoa(((struct sockaddr_in *)cursor->ifa_addr)->sin_addr)];
				} else {
                    addressStringForWWAN = [NSString stringWithUTF8String:
                                            inet_ntoa(((struct sockaddr_in *)cursor->ifa_addr)->sin_addr)];
                }
			}
			
			cursor = cursor->ifa_next;
		}
		freeifaddrs(addrs);
	}
    if (addressStringForWiFi) {
        return addressStringForWiFi;
    }
    if (!wifiEnabled) {
        return addressStringForWWAN;
    }
    return nil;
}

// return
//	 0: no connection
//	 1: celluar connection
//	 2: wifi connection
- (FBNetworkReachabilityConnectionMode)_getConnectionModeWithFlags:(SCNetworkReachabilityFlags)flags
{
	BOOL isReachable = ((flags & kSCNetworkFlagsReachable) != 0);
	BOOL needsConnection = ((flags & kSCNetworkFlagsConnectionRequired) != 0);
	if (isReachable && !needsConnection) {
		if ((flags & kSCNetworkReachabilityFlagsIsWWAN) != 0) {
            if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 7.0) {
                    CTTelephonyNetworkInfo *info = [[CTTelephonyNetworkInfo alloc] init];
                    NSString *currentRadioAccessTechnology = info.currentRadioAccessTechnology;
                    if (currentRadioAccessTechnology) {
                        if ([currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyLTE]) {
                            [info release];
                            return FBNetworkReachable4g;
                        } else if ([currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyEdge] || [currentRadioAccessTechnology isEqualToString:CTRadioAccessTechnologyGPRS]) {
                            [info release];
                            return FBNetworkReachable2g;
                        } else {
                            [info release];
                            return FBNetworkReachable3g;
                        }
                    }
                    [info release];
                }
                
                if ((flags & kSCNetworkReachabilityFlagsTransientConnection) == kSCNetworkReachabilityFlagsTransientConnection) {
                    if((flags & kSCNetworkReachabilityFlagsConnectionRequired) == kSCNetworkReachabilityFlagsConnectionRequired) {
                        return FBNetworkReachable2g;
                    }
                    return FBNetworkReachable3g;
                }
                return FBNetworkReachableWWAN;
            
        }
		
		if ([self _getIPAddressWiFilEnabled:YES]) {
            return FBNetworkReachableWiFi;
		}
		
	}
	return FBNetworkReachableNon;
}


- (void)_updateConnectionModeWithFlags:(SCNetworkReachabilityFlags)flags
{
	self.connectionMode = [self _getConnectionModeWithFlags:flags];
    self.ipaddress = [self _getIPAddressWiFilEnabled:NO];
    [self changeValueType];
}

// call back function
static void ReachabilityCallback(SCNetworkReachabilityRef target, SCNetworkReachabilityFlags flags, void* info)
{
	NSAutoreleasePool* myPool = [[NSAutoreleasePool alloc] init];
	
	FBNetworkReachability* noteObject = (FBNetworkReachability*)info;
	[noteObject _updateConnectionModeWithFlags:flags];
    NSLog(@"[INFO] Connection mode changed: %@ [%x]", noteObject, flags);

	[[NSNotificationCenter defaultCenter]
		postNotificationName:FBNetworkReachabilityDidChangeNotification object:noteObject];
    
	[myPool release];
}

- (BOOL)startNotifier
{
    [[NSNotificationCenter defaultCenter]
        postNotificationName:FBNetworkReachabilityDidChangeNotification
                      object:self];
    
	BOOL ret = NO;
	SCNetworkReachabilityContext context = {0, self, NULL, NULL, NULL};
	if(SCNetworkReachabilitySetCallback(reachability_, ReachabilityCallback, &context))
	{
		if(SCNetworkReachabilityScheduleWithRunLoop(
													reachability_, CFRunLoopGetCurrent(), kCFRunLoopDefaultMode))
		{
			ret = YES;
		}
	}
	return ret;
}	

- (void)stopNotifier
{
	if(reachability_!= NULL)
	{
		SCNetworkReachabilityUnscheduleFromRunLoop(reachability_, CFRunLoopGetCurrent(), kCFRunLoopDefaultMode);
	}
}


//------------------------------------------------------------------------------
#pragma mark -
#pragma mark API
//------------------------------------------------------------------------------
FBNetworkReachability* sharedInstance_ = nil;

+ (FBNetworkReachability*)sharedInstance
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance_ = [[self alloc] init];
    });

    return sharedInstance_;
}

+ (FBNetworkReachability*)networkReachabilityWithHostname:(NSString *)hostname{

    return self;
}

- (NSString*)IPAddress
{
    return [self _getIPAddressWiFilEnabled:NO];
}

- (void)refresh
{
    SCNetworkReachabilityFlags flags = 0;
    SCNetworkReachabilityGetFlags(reachability_, &flags);
    [self _updateConnectionModeWithFlags:flags];
}

//------------------------------------------------------------------------------
#pragma mark -
#pragma mark Properties
//------------------------------------------------------------------------------
- (BOOL)reachable
{
    if (self.connectionMode == FBNetworkReachableWiFi ||
        self.connectionMode == FBNetworkReachableWWAN) {
        return YES;
    } else {
        return NO;
    }
}

- (FBNetworkReachabilityConnectionMode)connectionMode
{
    if (connectionMode_ == FBNetworkReachableUninitialization) {
        [self refresh];
    }
    @synchronized (self) {
        return connectionMode_;
    }
}

- (void)setConnectionMode:(FBNetworkReachabilityConnectionMode)connectionMode
{
    @synchronized (self) {
        connectionMode_ = connectionMode;
    }
}

//------------------------------------------------------------------------------
#pragma mark -
#pragma mark etc
//------------------------------------------------------------------------------
- (NSString*)description
{
	NSString* desc = nil;
	
	switch (self.connectionMode) {
		case FBNetworkReachableUninitialization:
			desc = @"Not initialized";
			break;
            
		case FBNetworkReachableNon:
			desc = @"Not available";
			break;
			
		case FBNetworkReachableWWAN:
			desc = @"WWAN";
			break;
			
		case FBNetworkReachableWiFi:
			desc = @"WiFi";
			break;
            
        case FBNetworkReachable2g:
            desc = @"2G";
            break;
            
        case FBNetworkReachable3g:
            desc = @"3G";
            break;
            
        case FBNetworkReachable4g:
            desc = @"4G";
            break;
     
			
	}
	return desc;
}

- (int)changeValueType
{
    int type = 0;
    
     switch (self.connectionMode) {
         case FBNetworkReachableNon:
             type = NOT_CONNECT;
             break;
            
         case FBNetworkReachableWiFi:
             type = WIFI;
             break;
            
         case FBNetworkReachable2g:
             type = NETWORK_2G;
             break;
            
         case FBNetworkReachable3g:
             type = NETWORK_3G;
             break;
            
         case FBNetworkReachable4g:
             type = NETWORK_4G;
             break;
            
         default:
             type = NETWORK_4G;
             break;
            
     }
   // NSLog(@"type--------------%d",type);
     return type;


//    UIApplication *app = [UIApplication sharedApplication];
//
//    NSArray *children = [[[app valueForKeyPath:@"statusBar"] valueForKeyPath:@"foregroundView"] subviews];
//    int types = 0;
//    for (id child in children) {
//        if ([child isKindOfClass:NSClassFromString(@"UIStatusBarDataNetworkItemView")]) {
//            types = [[child valueForKeyPath:@"dataNetworkType"] intValue];
//        }
//    }
//        switch (types) {
//            case 0:
//                NSLog(@"No wifi or cellular");
//                type = NOT_CONNECT;
//                break;
//    
//            case 1:
//                NSLog(@"2G");
//                type = NETWORK_2G;
//                break;
//    
//            case 2:
//                NSLog(@"3G");
//                type = NETWORK_3G;
//                break;
//    
//            case 3:
//                NSLog(@"4G");
//                type = NETWORK_4G;
//                break;
//    
//            case 4:
//                NSLog(@"LTE");
//                type = NETWORK_4G;
//                break;
//    
//            case 5:
//                NSLog(@"Wifi");
//                type = WIFI;
//                break;  
//                  
//                  
//            default:  
//                break;  
//        }

//    CTTelephonyNetworkInfo *telephonyInfo = [[CTTelephonyNetworkInfo alloc] init];
//    CTCarrier *carrier = [telephonyInfo subscriberCellularProvider];
//    NSString *currentCountry=[carrier carrierName];
//    NSLog(@"%@",telephonyInfo.currentRadioAccessTechnology);
    //NSLog(@"[carrier isoCountryCode]==%@,[carrier allowsVOIP]=%d,[carrier mobileCountryCode=%@,[carrier mobileNetworkCode]=%@",[carrier isoCountryCode],[carrier allowsVOIP],[carrier mobileCountryCode],[carrier mobileNetworkCode]);

//    struct sockaddr_in zeroAddress;
//    bzero(&zeroAddress, sizeof(zeroAddress));
//    zeroAddress.sin_len = sizeof(zeroAddress);
//    zeroAddress.sin_family = AF_INET;
//    SCNetworkReachabilityRef defaultRouteReachability = SCNetworkReachabilityCreateWithAddress(NULL, (struct sockaddr *)&zeroAddress); //创建测试连接的引用：
//    SCNetworkReachabilityFlags flags;
//    SCNetworkReachabilityGetFlags(defaultRouteReachability, &flags);
//    if ((flags & kSCNetworkReachabilityFlagsReachable) == 0)
//    {
//        type = NOT_CONNECT;
//    }
//    if ((flags & kSCNetworkReachabilityFlagsConnectionRequired) == 0)
//    {
//        type = WIFI;
//
//    }
//    if ((((flags & kSCNetworkReachabilityFlagsConnectionOnDemand ) != 0) ||
//         (flags & kSCNetworkReachabilityFlagsConnectionOnTraffic) != 0))
//    {
//        if ((flags & kSCNetworkReachabilityFlagsInterventionRequired) == 0)
//        {
//            type = WIFI;
//        }
//    }
//    if ((flags & kSCNetworkReachabilityFlagsIsWWAN) == kSCNetworkReachabilityFlagsIsWWAN)
//    {
//        if((flags & kSCNetworkReachabilityFlagsReachable) == kSCNetworkReachabilityFlagsReachable) {
//            if ((flags & kSCNetworkReachabilityFlagsTransientConnection) == kSCNetworkReachabilityFlagsTransientConnection) {
//                type = NETWORK_3G;
//                if((flags & kSCNetworkReachabilityFlagsConnectionRequired) == kSCNetworkReachabilityFlagsConnectionRequired) {
//                    type = NETWORK_2G;
//
//                }
//            }
//        }
//    }
//        return type;
}




@end
