

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface ToolGetUUID : NSObject


+ (void)save:(NSString *)service data:(id)data;

+ (id)load:(NSString *)service;

+ (void)delete:(NSString *)service;

+ (NSString *)getIDFV;

@end
