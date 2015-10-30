using ConDep.implementation.Extensions;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.activities
{
    public class TracingActivity : CodeActivity
    {
        public InArgument<string> Message
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            TracingExtension extensionObj = context.GetExtension<TracingExtension>();
            if (extensionObj != null)
            {
                extensionObj.Messages.Add(Message.Get(context));
            }
        }
    }
}
