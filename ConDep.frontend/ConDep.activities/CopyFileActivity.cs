using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.IO;

namespace ConDep.activities
{

    public sealed class CopyFileActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> SourceDirectory { get; set; }
        public InArgument<string> TargetDirectory { get; set; }
        public InArgument<string> Filter { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            if(!Directory.Exists(SourceDirectory.Get(context)))
            {
                throw new DirectoryNotFoundException();
            }

            if (!Directory.Exists(TargetDirectory.Get(context)))
            {
                Directory.CreateDirectory(TargetDirectory.Get(context));
            }

            foreach(var item in Directory.EnumerateFiles(SourceDirectory.Get(context)))
            {
                var filename = Path.GetFileName(item);
                var targetFile = Path.Combine(TargetDirectory.Get(context), filename);
                File.Copy(item, targetFile);
            }
        }
    }
}
