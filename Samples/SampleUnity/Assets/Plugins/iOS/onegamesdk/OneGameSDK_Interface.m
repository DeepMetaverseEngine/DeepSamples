//
//  OneGameSDK_Interface.m
//  Unity-iPhone
//
//  Created by bowen.meng on 19/3/1.
//
//
#import "OneGameSDK.h"
#import "AlbumController.h"
#import "QCloudController.h"

void CallInitSDK()
{
    [[OneGameSDK sharedInstance] InitSDK];
}

void CallShowLogin(Boolean _is_auto_login)
{
    [[OneGameSDK sharedInstance] ShowLogin:_is_auto_login];
}

void CallShowLogout()
{
   [[OneGameSDK sharedInstance] ShowLogout];
}

void CallSwitchAccount()
{
    [[OneGameSDK sharedInstance] SwitchAccount];
}

void CallShowPersonCenter()
{
    
}


void CallHidePersonCenter()
{
    
}

void CallShowToolBar()
{
    
}

void CallHideToolBar()
{
    
}


char* CallPayItem(const char* _json_string)
{
    NSString *_json = [NSString stringWithFormat:@"%s", _json_string];
    [[OneGameSDK sharedInstance] PayItem:_json];
    NSString* info = @"success";
    const char* string = [info UTF8String];
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

int CallLoginState()
{
    return 0;
}

void CallShowShare(char* _json_string)
{

}

void CallSetPlayerInfo (char* _json_string)
{

}

char* CallGetUserData()
{
    const char* string = [@"" UTF8String];
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

char* CallGetPlatformData()
{
    NSString* jsonStr = [[OneGameSDK sharedInstance] GetPlatformData];
    const char* string = [jsonStr UTF8String];
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

void CallCopyClipboard(char* _json_string)
{

}

bool CallIsHasRequest(char* _json_string)
{
    return false;
}

void CallDestory()
{

}

void CallExitGame()
{

}


char* CallDoAnyFunction(char* _funName_string, char* _json_string)
{
    NSString *funName = [NSString stringWithFormat:@"%s", _funName_string];
    NSString *params = [NSString stringWithFormat:@"%s", _json_string];
    NSString* result = [[OneGameSDK sharedInstance] DoAnyFunction:funName withArgs:params];
    const char* string = [result UTF8String];
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

char* CallPhoneInfo()
{
    NSString* aid = @"";
    const char* string = [aid UTF8String];
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

void CallAddLocalPush(char* _json_data)
{

}

void CallRemoveLocalPush(char* _json_data)
{

}

void CallRemoveAllLocalPush()
{

}

void CallGetUserFriends()
{

}

void CallCreateCredentialProvider(char* appId, char* region, char* bucket, char* tmpSecretId, char* tmpSecretKey, char* sessionToken, long beginTime, long expiredTime)
{
    NSString *NSAppId = [NSString stringWithFormat:@"%s", appId];
    NSString *NSRegion = [NSString stringWithFormat:@"%s", region];
    NSString *NSBucket = [NSString stringWithFormat:@"%s", bucket];
    NSString *NSTmpSecretId = [NSString stringWithFormat:@"%s", tmpSecretId];
    NSString *NSTmpSecretKey = [NSString stringWithFormat:@"%s", tmpSecretKey];
    NSString *NSSessionToken = [NSString stringWithFormat:@"%s", sessionToken];
    [[QCloudController shareInstance] CreateCredentialProvider:NSAppId region:NSRegion bucket:NSBucket tmpSecretId:NSTmpSecretId tmpSecretKey:NSTmpSecretKey sessionToken:NSSessionToken beginTime:beginTime expiredTime:expiredTime];
    
}

void CallUpload(char* filePath, char* cosPath)
{
    NSString *nsFilePath = [NSString stringWithFormat:@"%s", filePath];
    NSString *nsCosPath = [NSString stringWithFormat:@"%s", cosPath];
    [[QCloudController shareInstance] Upload:nsFilePath cosPath:nsCosPath];
}

void CallCancel()
{
    NSLog(@"Canceled");
}

void CallOpenAlbum(char* title, int aspectX, int aspectY)
{
   [[AlbumController shareInstance] openAlbum:aspectX aspectY:aspectY];
}
