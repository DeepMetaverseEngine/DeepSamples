//
//  OneGameSDK.h
//  OneGameSDK
//
//  Created by LU YI on 2018/3/8.
//  Copyright © 2018年 bowen.meng. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "OneGameBaseBonjour.h"

#ifdef __cplusplus
#define OneGameSDK_EXTERN        extern "C" __attribute__((visibility ("default")))
#else
#define OneGameSDK_EXTERN        extern __attribute__((visibility ("default")))
#endif

@protocol OneGameSDKDelegate<NSObject>

-(UIView*) GetView;
-(UIViewController*) GetViewController;

@optional
//登陆通知
-(void) NotifyLogin:(NSNotification*)notification;
//登出通知
-(void) NotifyLogout:(NSNotification*)notification;
//支付结果通知
-(void) NotifyPayResult:(NSNotification*)notification;
//更新完成通知
-(void) NotifyUpdateFinish:(NSNotification*)notification;
//初始化完成通知
-(void) NotifyInitFinish:(NSNotification*)notification;
//重新登陆通知
-(void) NotifyRelogin:(NSNotification*)notification;
//本地推送
-(void) NotifyReceiveLocalPush:(NSNotification*)notification;
//玩家列表
-(void) NotifyUserFriends:(NSNotification*)notification;
//分享结果通知
-(void) NotifyShareResult:(NSNotification*)notification;
//自定义函数通知
-(void) NotifyExtraFunction:(NSNotification*)notification;

@end

@interface OneGameSDK : NSObject

@property (nonatomic, copy) NSDictionary* sdkParams;

+(OneGameSDK*) sharedInstance;
+(OneGameBaseBonjour*)GetPlatformInstance;

-(void) setDelegate:(id<OneGameSDKDelegate>)delegate;


-(UIView*) GetView;
-(UIViewController*) GetViewController;

-(void) initWithParams:(NSDictionary*)params;
-(void)InitSDK;
-(void)ShowLogin:(Boolean)_is_auto_login;
-(void)ShowLogout;
-(void)SwitchAccount;
-(NSString*)PayItem:(NSString*)_in_data;
-(void)ShowShare:(NSString*)_in_data;
-(NSString*)GetPlatformData;
-(NSString*)DoAnyFunction:(NSString*)_funcName withArgs:(NSString*)_json_string;

// UIApplicationDelegate事件
- (void)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions;
- (void)applicationWillResignActive:(UIApplication *)application;
- (void)applicationDidEnterBackground:(UIApplication *)application;
- (void)applicationWillEnterForeground:(UIApplication *)application;
- (void)applicationDidBecomeActive:(UIApplication *)application;
- (void)applicationWillTerminate:(UIApplication *)application;

// url处理
- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation;
- (BOOL)application:(UIApplication*)application handleOpenURL:(NSURL *)url;

@end

OneGameSDK_EXTERN NSString* const OneGameSDKNotifyLogin;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyLogout;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyPayResult;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyUpdateFinish;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyInitFinish;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyRelogin;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyReceiveLocalPush;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyUserFriends;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyShareResult;
OneGameSDK_EXTERN NSString* const OneGameSDKNotifyExtraFunction;
