using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Automation;
using SoftLattice.Core;
using SoftLattice.Core.Common;

namespace SoftLattice.Tests.Frame
{
    public class AppRunner
    {
        private App app;
        private readonly Thread appThread;
        private AutomationElement _userApplicationElement;

        private Exception capturedException;
        private readonly ProgrammaticStartupInfo info = new ProgrammaticStartupInfo();

        public AppRunner()
        {
            appThread = new Thread(delegate()
            {
                try
                {
                    app = new App();
                    app.Properties["override"] = info;
                    app.InitializeComponent();
                    app.Run();
                }
                catch (Exception x)
                {
                    capturedException = x;
                }
            });
            appThread.SetApartmentState(ApartmentState.STA);
        }

        public void Run()
        {
            appThread.Start();
            WaitForApplication();
        }

        public void SendMessageToApp(object msg)
        {
            MessageSender.SendMessage(msg);
        }

        private void WaitForApplication()
        {
            var aeDesktop = AutomationElement.RootElement;
            if (aeDesktop == null)
            {
                throw new Exception("Unable to get Desktop");
            }

            _userApplicationElement = null;
            do
            {
                if (capturedException != null)
                    throw capturedException;

                _userApplicationElement = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                                                                                  new PropertyCondition(
                                                                                      AutomationElement.
                                                                                          AutomationIdProperty,
                                                                                      "ShellView"));
                Thread.Sleep(500);
            } while ((_userApplicationElement == null || _userApplicationElement.Current.IsOffscreen));
        }



        public AutomationElement FindByAutomationId(string id)
        {
            return _userApplicationElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, id));
        }

        public TextPattern GetTextPattern(string automationId)
        {
            var ele = FindByAutomationId(automationId);
            object da;
            if (ele == null)
                return null;
            ele.TryGetCurrentPattern(TextPattern.Pattern, out da);
            return da as TextPattern;
        }

        public void AddPlugins(IEnumerable<Assembly> assemblies)
        {
            foreach (var a in assemblies)
              info.AddAssembly(a);
        }
    }
}