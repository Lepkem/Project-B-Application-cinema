using System;
using System.Collections.Generic;
using System.Text;

namespace ComingSoon
{
    public interface IComingSoon
    {
        string ShowComingSoon(string filename);
        string ShowComingSoon(string filename, int monthsUntilRelease); // this is an interface of overload in my library where the month which will determine whether you display the movie or not is variable
    }
}
