
namespace WcfService
{
    using System.Net;
    public static class Global
    {
        //all clients have to have minimum this version to work with ws
        //if don't ws give them information about it
        //the same nomenclature for all platforms! (apps can have their own snd versioning)
        public static double MinClientVer = 1.0;
        //it is used if sth go wrong during get version
        public const double DefWrongVer = -1.0;
        public const char VerTag = 'v';
        public const string TooOldClientTxt = "Too old client version";
        public const HttpStatusCode TooOldClientStatCode = HttpStatusCode.PreconditionFailed;

        //server and client support change it without update client:
        //have to be even numbers
        public const int MinMsgAmount = 8;
        public const int MaxMsgAmount = 32;

        //server and client NOT support change it without update client
        //default values
        public const string DefaultPrevId = "0";
        public const string DefaultIsNext = "false";
        public const string DefaultPrevDate = "9999-12-31 23:59:59";
        public const int DefaultNotiFreq = 120;

        //validation (length)
        public const int UserLoginMaxLength = 22;
        public const int UserLoginMinLength = 4;
        //for future not use now
        public const int UserEmailMaxLength = 40;
        //for future not use now
        public const int UserEmailMinLength = 6; 
        public const int RhymeTitleMaxLength = 35;
        public const int RhymeTitleMinLength = 3;
        public const int RhymeMsgMaxLength = 48;
        public const int RhymeMsgMinLength = 6;

        
    }
}