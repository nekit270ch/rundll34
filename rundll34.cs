using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DllCallerLib;

namespace RunDLL34{
    class RunDLL34{
        private static string ShortTypeNameToFullName(string sn){
            string name = sn;
            if(name == "int") name = "System.Int32";
            else if(name == "uint") name = "System.UInt32";
            else if(name == "long") name = "System.Int64";
            else if(name == "ulong") name = "System.UInt64";
            else if(name == "double") name = "System.Double";
            else if(name == "byte") name = "System.Byte";
            else if(name == "intptr") name = "System.IntPtr";
            else if(name == "char") name = "System.Char";
            else if(name == "string") name = "System.String";
            return name;
        }

        public static void Main(string[] args){
            var funcArgs = new List<Argument>();

            if(args.Length < 3){
                Console.WriteLine("Usage: rundll34 <dll name> <function name> <return type> [arguments: <type>:<value>]");
                Environment.Exit(1);
            }

            if(args.Length > 3){
                foreach(string ar in args.Skip(3)){
                    string[] spl = ar.Split(':');

                    IntPtr ptr = IntPtr.Zero;
                    bool isPtr = false;

                    if(ar == "null"){
                        isPtr = true;
                        spl = new string[]{ "System.IntPtr", "" };
                    }else if(spl.Length < 2){
                        Console.WriteLine("Invalid format");
                        Environment.Exit(1);
                    }

                    if(spl[0] == "alloc"){
                        spl[0] = "System.IntPtr";
                        ptr = Marshal.AllocHGlobal(Marshal.SizeOf(Type.GetType(ShortTypeNameToFullName(spl[1]))));
                        isPtr = true;
                    }

                    spl[0] = ShortTypeNameToFullName(spl[0]);

                    try{
                        funcArgs.Add(
                            new Argument(
                                spl[0],
                                isPtr?ptr:(spl[0]=="System.IntPtr"?new IntPtr(int.Parse(spl[1])):Convert.ChangeType(String.Join(":", spl.Skip(1)), Type.GetType(spl[0])))
                            )
                        );
                    }catch(FormatException){
                        Console.WriteLine("Invalid format");
                        Environment.Exit(1);
                    }catch(Exception e){
                        Console.Write("Error: ");
                        Console.WriteLine(e);
                        Environment.Exit(1);
                    }
                }
            }

            try{
                string type = args[2];
                if(type == "void"){
                    DllCaller.CallFunction(args[0], args[1], "System.Int32", funcArgs);
                }else{
                    Console.WriteLine(DllCaller.CallFunction(args[0], args[1], ShortTypeNameToFullName(type), funcArgs));
                }
            }catch(ArgumentNullException){
                Console.WriteLine("Invalid DLL or function name");
                Environment.Exit(1);
            }catch(Exception e){
                Console.Write("Error: ");
                Console.WriteLine(e);
                Environment.Exit(1);
            }
        }
    }
}