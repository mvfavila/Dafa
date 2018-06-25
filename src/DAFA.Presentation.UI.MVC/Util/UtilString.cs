using System;

namespace DAFA.Presentation.UI.MVC.Util
{
    public static class UtilString
    {
        public static string AddEmailMask(string email)
        {
            try
            {
                var sides = email.Split('@');
                if (sides.Length <= 1)
                    return email;
                var leftSide = sides[0];
                leftSide = leftSide[0] + new string('*', leftSide.Length - 2) + leftSide[leftSide.Length - 1];
                var rightSide = "@" + sides[1];

                return leftSide + rightSide;
            }
            catch (IndexOutOfRangeException)
            {
                const string msg = "Invalid argument format. A valid e-mail format is required";
                throw new ArgumentException(msg, nameof(email));
            }
        }
    }
}