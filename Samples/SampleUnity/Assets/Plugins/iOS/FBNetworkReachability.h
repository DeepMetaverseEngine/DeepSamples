#import <Foundation/Foundation.h>
#import <SystemConfiguration/SystemConfiguration.h>

typedef enum {
	FBNetworkReachableUninitialization = 0,
	FBNetworkReachableNon,
	FBNetworkReachableWiFi,
	FBNetworkReachableWWAN,
    FBNetworkReachable2g,
    FBNetworkReachable3g,
    FBNetworkReachable4g
} FBNetworkReachabilityConnectionMode;

#define FBNetworkReachabilityDidChangeNotification @"FBNetworkReachabilityDidChangeNotification"

@interface FBNetworkReachability : NSObject {

	SCNetworkReachabilityRef reachability_;
}

// API
@property (assign, readonly) FBNetworkReachabilityConnectionMode connectionMode;
@property (assign, readonly) BOOL reachable;
@property (copy, readonly) NSString* ipaddress;
+ (FBNetworkReachability*)sharedInstance;
- (void)refresh;
- (BOOL)startNotifier;
- (void)stopNotifier;
+ (FBNetworkReachability*)networkReachabilityWithHostname:(NSString *)hostname;
- (int)changeValueType;
@end
