#import "IosPlatform.h"
#import "FBNetworkReachability.h"
#import "ToolGetUUID.h"
#include <sys/utsname.h>
#import <AdSupport/AdSupport.h>

#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

extern void UnitySendMessage(const char *, const char *, const char *);

extern "C" {
    const char * getSystemVersion(){
        const char * version = [[[IosPlatform sharedManager]
         getSystemVersion] UTF8String];
        return version;
    }
    
    
    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        
        return res;
    }
    

	///	获取设备类型.
    char* _getDeviceType()
	{
        struct utsname systemInfo;
        uname(&systemInfo);
        NSString *device = [NSString stringWithCString:systemInfo.machine encoding:NSUTF8StringEncoding];
        return cStringCopy([device UTF8String]);
	}

	///	获取唯一号.
	char* _getDeviceUUID()
	{
        NSString* uuid = [ToolGetUUID getIDFV];
        return cStringCopy([uuid UTF8String]);
	}
    
    ///	获取网络状态.
    int _getNetworkStatus(){
        FBNetworkReachability *networkStatusMgr = [FBNetworkReachability sharedInstance];
        [networkStatusMgr refresh];
        return [networkStatusMgr changeValueType];
    }
    
    /// 获取信号强度.
    //int CTGetSignalStrength();
    int _getSignalStrength(){
        return 1;
        //return CTGetSignalStrength();
    }
    
    
    ///获取设备电量
    float _getiOSBatteryLevel()
    {
        [[UIDevice currentDevice] setBatteryMonitoringEnabled:YES];
        float level = [[UIDevice currentDevice] batteryLevel];
        if(level != -1)
        {
            level = level * 100;
        }
        return level;
    }
    
    char* _getUserAgent()
    {
        NSString* agent = @"deprecated";

        return cStringCopy([agent UTF8String]);
    }
    
    int _getScreenScaleFactor()
    {
        int notch = 0;
        if (@available(iOS 11.0, *)) {
            int orientation = [[UIDevice currentDevice] orientation];
            UIWindow *window = UIApplication.sharedApplication.keyWindow;
            CGFloat topPadding = 0;
            CGFloat bottomPadding = 0;
            if(orientation == UIDeviceOrientationLandscapeLeft || orientation == UIDeviceOrientationLandscapeRight){
                topPadding = window.safeAreaInsets.left;
                bottomPadding = window.safeAreaInsets.right;
            }else {
                topPadding = window.safeAreaInsets.top;
                bottomPadding = window.safeAreaInsets.bottom;
            }
            if(topPadding > 20 || bottomPadding > 20){
                CGFloat scale = [UIScreen mainScreen].scale;
                notch = scale;
            }
        }
        return notch;
    }
}



@implementation IosPlatform

static IosPlatform * shardDialogManager;

+ (IosPlatform*) sharedManager {
    @synchronized(self) {
        if(shardDialogManager == nil) {
            shardDialogManager = [[self alloc]init];
            
            shardDialogManager.userAgent = @"";
        }
    }
    return shardDialogManager;
}


//获得系统版本.
-(NSString *) getSystemVersion
{
    return [[UIDevice currentDevice] systemVersion];
}

// 获取当前任务所占用的内存（单位：MB）.
- (double)usedMemory
{
	task_basic_info_data_t taskInfo;
	mach_msg_type_number_t infoCount = TASK_BASIC_INFO_COUNT;
	kern_return_t kernReturn = task_info(	mach_task_self(), 
											TASK_BASIC_INFO, 
											(task_info_t)&taskInfo, 
											&infoCount);


	if (kernReturn != KERN_SUCCESS) 
		return NSNotFound;
  
	return taskInfo.resident_size / 1024.0 / 1024.0;

}


/*by zzy begin*/
- (void) setApnsID:(NSData*)apns
{
    m_apnsid = apns;
}

- (NSString*) getApnsID
{
 
    NSString* base64 = nil;
    
    if (m_apnsid)
    {
        
        if ([m_apnsid respondsToSelector:@selector(base64EncodedStringWithOptions:)])
        {
            base64 = [m_apnsid base64EncodedStringWithOptions:kNilOptions];
        }
        else
        {
            base64 = [m_apnsid base64EncodedStringWithOptions:0];
        }
        
    }

    NSLog(@"get apns id====%@ ,base=%@" , m_apnsid , base64);
    
    return base64;
}

- (void) setCurBrightness:(CGFloat) brightness
{
    m_curBrightness = brightness;
}

- (CGFloat) getCurBrightness
{
    return m_curBrightness;
}

- (void) setNewBrightness:(CGFloat) brightness
{
    m_newBrightness = brightness;
}

- (CGFloat) getNewBrightness
{
    return m_newBrightness;
}

@end
