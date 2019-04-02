using System;

namespace ConsoleMario.Exceptions.UtilityExceptions
{
    public class NoMoreLevelException : Exception
    {
        public NoMoreLevelException() : base("There are no more level yet") { }
    }
}
