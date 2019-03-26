using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Utils
{
    public enum AccountStatusCodes
    {
        Ok = 1,
        Failed = 2,        
        UserLocked = 3,
        WrongCredentials = 4,
        UserNotExist = 5
    }
}
