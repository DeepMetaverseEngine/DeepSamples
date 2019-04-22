
#import <Foundation/Foundation.h>
#include <mach/mach.h>

@interface IosPlatform : NSObject<UIAlertViewDelegate , UIWebViewDelegate> {

    CGFloat m_curBrightness;
    CGFloat m_newBrightness;
    NSData* m_apnsid;
}

//@property (nonatomic , copy) UIWebView* webViwe;
@property (nonatomic , copy) NSString* userAgent;
@property (nonatomic, copy) NSString* update_url;


+ (IosPlatform*) sharedManager;
- (void) showSelectDialog:(NSString *)msg confirm:(NSString *)confirmStr cancel:(NSString *)cancelStr;
- (void) showSelectDialog:(NSString *)title message:(NSString*)msg confirm:(NSString *)confirmStr cancel:(NSString *)cancelStr;
- (void) showSubmitDialog:(NSString *)msg confirm:(NSString *)confirmStr;
- (void) showSubmitDialog:(NSString *)title message:(NSString*)msg confirm:(NSString *)confirmStr;


-(NSString *) getSystemVersion;//获得系统版本.

-(double) usedMemory;

- (void) setCurBrightness:(CGFloat) brightness;

- (CGFloat) getCurBrightness;

- (void) setNewBrightness:(CGFloat) brightness;

- (CGFloat) getNewBrightness;

- (void) createHttpRequest;

- (NSString*) getUserAgent;

- (void) showUpdateAlert:(int) type url:(NSString*) url;

- (void) setApnsID:(NSData*)apns;

- (NSString*) getApnsID;


@end
