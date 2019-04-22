//
//  OneGameSDKAppController.m
//  OneGameSDK
//
//  Created by LU YI on 2018/3/23.
//  Copyright © 2018年 bowen.meng. All rights reserved.
//
#import "UnityAppController.h"
#import "OneGameSDK.h"

#define CALLBACK_GAMEOBJECT_NAME "OneGameSDK"
extern void UnitySendMessage(const char *, const char *, const char *);

//登陆通知
const char* NOTIFY_LOGIN = "NotifyLogin";
//登出通知
const char* NOTIFY_LOGOUT = "NotifyLogout";
//支付结果通知
const char* NOTIFY_PAY_RESULT = "NotifyPayResult";
//更新完成通知
const char* NOTIFY_UPDATE_FINISH = "NotifyUpdateFinish";
//初始化完成通知
const char* NOTIFY_INIT_FINISH = "NotifyInitFinish";
//重新登陆通知
const char* NOTIFY_RELOGIN = "NotifyRelogin";
//本地推送
const char* NOTIFY_RECEIVE_LOCA_PUSH = "NotifyReceiveLocalPush";
//玩家列表
const char* NOTIFY_USER_FRIENDS = "NotifyUserFriends";
//分享结果通知
const char* NOTIFY_SHARE_RESULT = "NotifyShareResult";
//自定义函数通知
const char* NOTIFY_EXTRA_FUNCTION = "NotifyExtraFunction";

#if defined(__cplusplus)
extern "C"{
#endif
    UIViewController* UnityGetGLViewController();
    UIView* UnityGetGLView();
    extern void UnitySendMessage(const char* gameObjectName, const char* methodName, const char* param);
#if defined(__cplusplus)
}
#endif

@interface OneGameSDKAppController : UnityAppController<OneGameSDKDelegate>

@end

@implementation OneGameSDKAppController

- (void)showAlertView:(NSString *)message{
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"" message:message delegate:self cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
    [alert show];
}

-(void) SendCallback:(const char*)method withParams:(NSDictionary *)params
{
    NSLog(@"SendCallback method=%s params=%@ ",method,params);
    NSString* jsStr = nil;
    if (params)
    {
        NSError* error;
        NSData* data = [NSJSONSerialization dataWithJSONObject:params options:kNilOptions error:&error];
        if (data)
        {
            jsStr = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        }
    }
    if (jsStr)
        UnitySendMessage(CALLBACK_GAMEOBJECT_NAME, method, [jsStr UTF8String]);
    else
        UnitySendMessage(CALLBACK_GAMEOBJECT_NAME, method, "");
}

/**
 OneGameSDKDelegate
 */

-(UIView*) GetView
{
    return UnityGetGLView();
}

-(UIViewController*) GetViewController
{
    return UnityGetGLViewController();
}

-(void) NotifyInitFinish:(NSNotification*)notification
{
    NSMutableDictionary* params = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_INIT_FINISH withParams:params];
}

-(void) NotifyLogin:(NSNotification*)notification
{
    NSMutableDictionary* params = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_LOGIN withParams:params];
}


-(void) NotifyLogout:(NSNotification*)notification
{
    [self SendCallback:NOTIFY_LOGOUT withParams:notification.userInfo];
}

-(void) NotifyPayResult:(NSNotification*)notification
{
    NSMutableDictionary* info = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_PAY_RESULT withParams:info];
}

-(void) NotifyUpdateFinish:(NSNotification*)notification
{
    [self SendCallback:NOTIFY_UPDATE_FINISH withParams:notification.userInfo];
}

-(void) NotifyRelogin:(NSNotification*)notification
{
    NSMutableDictionary* info = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_RELOGIN withParams:info];
}

-(void) NotifyReceiveLocalPush:(NSNotification*)notification
{
    NSMutableDictionary* info = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_RECEIVE_LOCA_PUSH withParams:info];
}

-(void) NotifyUserFriends:(NSNotification*)notification
{
    NSMutableDictionary* info = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_USER_FRIENDS withParams:info];
}

-(void) NotifyShareResult:(NSNotification*)notification
{
    NSMutableDictionary* info = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_SHARE_RESULT withParams:info];
}

-(void) NotifyExtraFunction:(NSNotification *)notification
{
    NSMutableDictionary* info = [NSMutableDictionary dictionaryWithDictionary:notification.userInfo];
    [self SendCallback:NOTIFY_EXTRA_FUNCTION withParams:[info valueForKey:@"params"]];
}

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    NSFileManager *fileManager = [NSFileManager defaultManager];
    NSString *configPath = [[[NSBundle mainBundle] resourcePath] stringByAppendingString:@"/Data/Raw/CPSettings.txt"];
    if ([fileManager fileExistsAtPath: configPath ]){
        NSString *jsonString = [NSString stringWithContentsOfFile:configPath encoding:NSUTF8StringEncoding error:nil];
        NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        NSError *err;
        NSDictionary *jsonConfig = [NSJSONSerialization JSONObjectWithData:jsonData
                                                                   options:NSJSONReadingMutableContainers
                                                                     error:&err];
        if(err) {
            [self showAlertView:@"Load config file failed."];
        }
        [[OneGameSDK sharedInstance] setDelegate:self];
        [[OneGameSDK sharedInstance] initWithParams:jsonConfig];
    }else{
        NSLog(@" File %@ doesn't exist. Skip the sdk initialize progress.", configPath);
    }

    BOOL ret = [super application:application didFinishLaunchingWithOptions:launchOptions];
    
    [[OneGameSDK sharedInstance] application:application didFinishLaunchingWithOptions:launchOptions];
    return ret;
}

- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation
{
    BOOL ret = [super application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    
    [[OneGameSDK sharedInstance] application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    
    return ret;
}


- (void)applicationWillResignActive:(UIApplication *)application
{
    [super applicationWillResignActive:application];
    [[OneGameSDK sharedInstance] applicationWillResignActive:application];
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    [super applicationDidEnterBackground:application];
    [[OneGameSDK sharedInstance] applicationDidEnterBackground:application];
}
- (void)applicationWillEnterForeground:(UIApplication *)application
{
    [super applicationWillEnterForeground:application];
    [[OneGameSDK sharedInstance] applicationWillEnterForeground:application];
}
- (void)applicationDidBecomeActive:(UIApplication *)application;
{
    application.applicationIconBadgeNumber = 0;
    [super applicationDidBecomeActive:application];
    [[OneGameSDK sharedInstance] applicationDidBecomeActive:application];
}
- (void)applicationWillTerminate:(UIApplication *)application
{
    [super applicationWillTerminate:application];
    [[OneGameSDK sharedInstance] applicationWillTerminate:application];
}
- (void)applicationDidReceiveMemoryWarning:(UIApplication*)application
{
    [super applicationDidReceiveMemoryWarning:application];
    UnitySendMessage("GameGlobal", "UnloadResource", "");
}

@end

IMPL_APP_CONTROLLER_SUBCLASS (OneGameSDKAppController)
