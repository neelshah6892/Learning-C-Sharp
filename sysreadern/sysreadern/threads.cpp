
#include "stdafx.h"

using namespace System;
using namespace System::Threading;
ref 
class Example
{
public:

   // This thread procedure performs the task.
   static void ThreadProc( Object^ stateInfo )
   {

      // No state object was passed to QueueUserWorkItem, so 
      // stateInfo is 0.
      Console::WriteLine( "Hello from the thread pool." );
   }

};

int main()
{

   // Queue the task.
   ThreadPool::QueueUserWorkItem( gcnew WaitCallback( Example::ThreadProc ) );
   Console::WriteLine( "Main thread does some work, then sleeps." );

   // If you comment out the Sleep, the main thread exits before
   // the thread pool task runs.  The thread pool uses background
   // threads, which do not keep the application running.  (This
   // is a simple example of a race condition.)
   Thread::Sleep( 1000 );
   Console::WriteLine( "Main thread exits." );
   return 0;
}

