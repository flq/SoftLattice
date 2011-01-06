using System;

namespace SoftLattice.Common
{
    /// <summary>
    /// This is an attribute you may use for pure documentation purposes, i.e. no functionality 
    /// is coupled with this metadata. 
    /// Messages can be any kind of
    /// object, such that it may be difficult to find out e.g. which
    /// objects are used as messages. If you mark your messages with this attribute you get a list of all objects that act as messages
    /// by searching where this attribute is used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MessageAttribute : Attribute
    {
        
    }
}