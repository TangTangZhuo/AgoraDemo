#import <AVFoundation/AVFoundation.h>

#define STREQUAL(VAR, CONSTANT) strcmp(VAR, CONSTANT) == 0

void iOSAudio_dumpAudioSession()
{
    AVAudioSession* session = [AVAudioSession sharedInstance];
    NSLog(@"Audio Category: %@", session.category);
    NSLog(@"Audio Mode: %@", session.mode);
    NSLog(@"Audio Option: %lu", session.categoryOptions);
}

void iOSAudio_setupAudioSession(const char *category)
{
    NSString *nsCategory = nil;
    if(STREQUAL(category, "ambient"))
    {
        nsCategory = AVAudioSessionCategoryAmbient;
    }
    else if(STREQUAL(category, "solo"))
    {
        nsCategory = AVAudioSessionCategorySoloAmbient;
    }
    else if(STREQUAL(category, "playback"))
    {
        nsCategory = AVAudioSessionCategoryPlayback;
    }
    else if(STREQUAL(category, "record"))
    {
        nsCategory = AVAudioSessionCategoryRecord;
    }
    else if(STREQUAL(category, "playAndRecord"))
    {
        nsCategory = AVAudioSessionCategoryPlayAndRecord;
    }
    
    AVAudioSession* session = [AVAudioSession sharedInstance];
    NSError* error = nil;
    [session setCategory:nsCategory error:&error];
    [session setMode:AVAudioSessionModeDefault error:&error];
}

#undef STREQUAL
