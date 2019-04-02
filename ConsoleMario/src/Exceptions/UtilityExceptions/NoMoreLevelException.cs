using System;

namespace ConsoleMario.Exceptions.UtilityExceptions
{
    public class NoMoreLevelException : Exception
    {
        #region Public Constructors

        public NoMoreLevelException() : base("There are no more level yet") { }

        #endregion Public Constructors
    }
}
