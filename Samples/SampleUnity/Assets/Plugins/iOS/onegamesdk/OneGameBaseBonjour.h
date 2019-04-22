//
//  OneGameBaseBonjour.h
//  OneGameSDK
//
//  Created by LU YI on 2018/3/9.
//  Copyright © 2018年 bowen.meng. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BaseBonjourDelegate.h"

#define DEFINE_NOTIFICATION(name) __attribute__((visibility ("default"))) NSString* const name = @#name;

DEFINE_NOTIFICATION(OneGameSDKNotifyLogin);
DEFINE_NOTIFICATION(OneGameSDKNotifyLogout);
DEFINE_NOTIFICATION(OneGameSDKNotifyPayResult);
DEFINE_NOTIFICATION(OneGameSDKNotifyUpdateFinish);
DEFINE_NOTIFICATION(OneGameSDKNotifyInitFinish);
DEFINE_NOTIFICATION(OneGameSDKNotifyRelogin);
DEFINE_NOTIFICATION(OneGameSDKNotifyReceiveLocalPush);
DEFINE_NOTIFICATION(OneGameSDKNotifyUserFriends);
DEFINE_NOTIFICATION(OneGameSDKNotifyShareResult);
DEFINE_NOTIFICATION(OneGameSDKNotifyExtraFunction);


@interface OneGameBaseBonjour : NSObject<BaseBonjourDelegate>

-(void) eventUserLogin:(NSDictionary*) params;
-(void) eventUserLogout:(NSDictionary*) params;
-(void) eventPayResult:(NSDictionary*) params;
-(void) eventUpdateFinish:(NSDictionary*) params;
-(void) eventInitFinish:(NSDictionary*) params;
-(void) eventRelogin:(NSDictionary*) params;
-(void) eventReceiveLocalPush:(NSDictionary*) params;
-(void) eventUserFriends:(NSDictionary*) params;
-(void) eventShareResult:(NSDictionary*) params;
-(void) eventExtraFunction:(NSString*)name params:(NSDictionary*)params;
@end
