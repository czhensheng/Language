using System;

namespace IDisposableDemo {
    class Program {
        static void Main (string[] args) {
            System.Console.WriteLine ("using做法");
            //使用using会自动调用Dispose方法
            using (CaryClass caryClass = new CaryClass ()) {
                caryClass.DoSomeThing ();
            }
            System.Console.WriteLine ("现实做法（手动调用Dispose方法）");
            CaryClass caryClass2 = new CaryClass ();
            try {
                caryClass2.DoSomeThing ();
            } finally {
                IDisposable disposable = caryClass2 as IDisposable;
                if (disposable != null) {
                    disposable.Dispose ();
                }
            }

            Console.ReadLine ();
        }
    }

    public class CaryClass : IDisposable {
        public void Dispose () {
            System.Console.WriteLine ("及时释放资源");
        }

        public void DoSomeThing () {
            System.Console.WriteLine ("do something");
        }
    }

    public class DisposableClass : IDisposable {
        bool _disposed; //是否回收完成 
        /// <summary>
        /// 当需要回收非托管资源的DisposableClass类，就调用Dispoase()方法。而这个方法不会被CLR自动调用，需要手动调用。
        /// </summary>
        public void Dispose () {
                Dispose (true);
                //请求系统不要调用指定对象的终结器
                GC.SuppressFinalize (this);
            }
            /// <summary>
            /// 析构函数
            /// 当托管堆上的对象没有被其它对象引用，GC会在回收对象之前，调用对象的析构函数
            /// 告诉GC可以回收
            /// </summary>
            /// <returns></returns>
            ~DisposableClass () {
                Dispose (false);//GC回收的时候，就不需要手动回收了
            }
        /// <summary>
        /// 所有的托管和非托管资源都能被回收
        /// </summary>
        /// <param name="disposing">是否需要释放实现IDisposable接口的托管对象</param>
        protected virtual void Dispose (bool disposing) {
            if (_disposed) {
                return;
            }
            if (disposing) {
                //释放实现IDisposable接口的托管对象
            }
            //释放非托管资源 ，将其设置为null就OK
            _disposed = true;
        }
    }
}