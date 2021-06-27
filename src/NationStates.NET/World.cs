namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public static class World
    {
        public static Dispatch GetDispatch(ulong id)
        {
            return new Dispatch(id);
        }
    }
}
