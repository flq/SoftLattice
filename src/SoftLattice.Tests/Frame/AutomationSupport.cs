using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;

namespace SoftLattice.Tests.Frame
{
    public class AutomationSupport
    {
        private readonly AutomationElement _root;

        public AutomationSupport(AutomationElement root)
        {
            _root = root;
        }

        public AutomationElement FindByName(string xName)
        {
            var searchStack = new Stack<AutomationElement>();

            if (NameEquals(_root, xName))
                return _root;

            var currentElement = _root;

            AddAllChildsToStack(currentElement, searchStack);

            while (searchStack.Count > 0)
            {
                var element = searchStack.Pop();
                if (NameEquals(element, xName))
                    return element;
                AddAllChildsToStack(element, searchStack);
            }

            return null;
        }

        private static void AddAllChildsToStack(AutomationElement currentElement, Stack<AutomationElement> searchStack)
        {
            foreach (var ae in AllChildsOf(currentElement))
                searchStack.Push(ae);
        }

        private static bool NameEquals(AutomationElement automationElement, string xName)
        {
            return xName.Equals(automationElement.Current.AutomationId, StringComparison.InvariantCultureIgnoreCase);
        }

        private static IEnumerable<AutomationElement> AllChildsOf(AutomationElement element)
        {
            var firstChild = TreeWalker.ControlViewWalker.GetFirstChild(element);
            if (firstChild == null)
                yield break;
            yield return firstChild;

            var currentSibling = firstChild;
            while (currentSibling != null)
            {
                currentSibling = TreeWalker.ControlViewWalker.GetNextSibling(element);
                if (currentSibling != null)
                    yield return currentSibling;
            }
        }
    }
}