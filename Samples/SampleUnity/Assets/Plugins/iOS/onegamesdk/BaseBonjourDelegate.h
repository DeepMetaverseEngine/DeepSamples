//
//  BaseBonjourDelegate
//  OneGameSDK
//
//  Created by LU YI on 2018/3/8.
//  Copyright © 2018年 bowen.meng. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@protocol BaseBonjourDelegate

@required
/// 初始化
-(void)InitSDK:(NSString*)_in_data;
/// 登陆
-(void)ShowLogin:(Boolean)_is_auto_login;
/// 登出
-(void)ShowLogout;
///切换账号
-(void)SwitchAccount;
/// 支付
-(NSString*)PayItem:(NSString*)_in_data;
/// 分享
-(void)ShowShare:(NSString*)_in_data;

@optional
/// 用户信息
-(NSString*)GetUserData;
/// 渠道信息
-(NSString*)GetPlatformData;
/// 自定义函数
-(NSString*)DoAnyFunction:(NSString*)_funcName withArgs:(NSString*)_json_string;
/// 退出游戏
-(void)ExitGame;
/// 玩家信息上报
-(void)SetPlayerInfo:(NSString*)_in_data;





- (void)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions;
- (void)applicationDidBecomeActive:(UIApplication *)application;
- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;

@end
