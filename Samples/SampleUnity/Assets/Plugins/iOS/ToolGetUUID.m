

#import "ToolGetUUID.h"

@implementation ToolGetUUID

+ (NSString *) getIDFA
{
    NSMutableString *idStr = [NSMutableString stringWithFormat:@""];
//    NSString* strFV = @"NULL";
//    NSString* strFa = @"NULL";
//    
//        [idStr appendString:[NSString stringWithFormat:@"%@\",\"idfa\":\"", strFV]];
//    
//    NSUUID *idFa = [[ASIdentifierManager sharedManager] advertisingIdentifier];
//    if(idFa)
//    {
//        strFa = [idFa UUIDString];
//    }
//    [idStr appendString:[NSString stringWithFormat:@"%@\"}", strFa]];
    
    return idStr;
}



+ (NSString *)getIDFV
{

    //定义存入keychain中的账号 也就是一个标识 表示是某个app存储的内容   bundle id就好
    NSString * const KEY_USERNAME_PASSWORD = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleVersion"];
    //@"com.jimei.fox";
    NSString * const KEY_PASSWORD = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleVersion"];
    //@"com.jimei.fox";

    
    //测试用 清除keychain中的内容
    //[ToolGetUUID delete:KEY_USERNAME_PASSWORD];
    
    //读取账号中保存的内容
    NSMutableDictionary *readUserPwd = (NSMutableDictionary *)[ToolGetUUID load:KEY_USERNAME_PASSWORD];
    NSLog(@"keychain------><>%@",readUserPwd);

    if (!readUserPwd)
    {

        //如果为空 说明是第一次安装 做存储操作
        NSString *identifierStr = [[[UIDevice currentDevice] identifierForVendor] UUIDString];
        NSLog(@"identifierStr-----><>%@",identifierStr);

        
        NSMutableDictionary *usernamepasswordKVPairs = [NSMutableDictionary dictionaryWithObject:identifierStr forKey:KEY_PASSWORD];
        [ToolGetUUID save:KEY_USERNAME_PASSWORD data:usernamepasswordKVPairs];

        return identifierStr;

    }
    else
    {
        return [readUserPwd objectForKey:KEY_PASSWORD];
    }
}


//储存

+ (void)save:(NSString *)service data:(id)data {

    //Get search dictionary
    NSMutableDictionary *keychainQuery = [self getKeychainQuery:service];

    //Delete old item before add new item
    SecItemDelete((__bridge CFDictionaryRef)keychainQuery);

    //Add new object to search dictionary(Attention:the data format)
    if (@available(iOS 11.0, *)) {
        [keychainQuery setObject:[NSKeyedArchiver archivedDataWithRootObject:data requiringSecureCoding:YES error:nil] forKey:(__bridge id)kSecValueData];
    } else {
        [keychainQuery setObject:[NSKeyedArchiver archivedDataWithRootObject:data] forKey:(__bridge id)kSecValueData];
    }

    //Add item to keychain with the search dictionary
    SecItemAdd((__bridge CFDictionaryRef)keychainQuery, NULL);

}

 

+ (NSMutableDictionary *)getKeychainQuery:(NSString *)service {

    return [NSMutableDictionary dictionaryWithObjectsAndKeys:

            (__bridge id)kSecClassGenericPassword,(__bridge id)kSecClass,

            service, (__bridge id)kSecAttrService,

            service, (__bridge id)kSecAttrAccount,

            (__bridge id)kSecAttrAccessibleAfterFirstUnlock,(__bridge id)kSecAttrAccessible,

            nil];

}

 

//取出

+ (id)load:(NSString *)service {

    id ret = nil;

    NSMutableDictionary *keychainQuery = [self getKeychainQuery:service];

    //Configure the search setting

    //Since in our simple case we are expecting only a single attribute to be returned (the password) we can set the attribute kSecReturnData to kCFBooleanTrue

    [keychainQuery setObject:(__bridge id)kCFBooleanTrue forKey:(__bridge id)kSecReturnData];

    [keychainQuery setObject:(__bridge id)kSecMatchLimitOne forKey:(__bridge id)kSecMatchLimit];

    CFDataRef keyData = NULL;

    if (SecItemCopyMatching((__bridge CFDictionaryRef)keychainQuery, (CFTypeRef *)&keyData) == noErr) {


        ret = [NSKeyedUnarchiver unarchiveObjectWithData:(__bridge NSData *)keyData];


    }

    if (keyData)

        CFRelease(keyData);

    return ret;

}

 

//删除

+ (void)delete:(NSString *)service {

    NSMutableDictionary *keychainQuery = [self getKeychainQuery:service];

    SecItemDelete((__bridge CFDictionaryRef)keychainQuery);

}

@end
