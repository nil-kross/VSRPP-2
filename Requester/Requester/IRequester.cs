using System;
using System.Collections.Generic;

namespace Lomtseu
{
    public interface IRequester
    {
        String GetResponseString(Uri uri);

        T GetResponseItem<T>(Uri uri);

        IEnumerable<T> GetResponseItems<T>(Uri uri);
    }
}