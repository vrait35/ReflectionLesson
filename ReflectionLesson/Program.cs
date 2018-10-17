using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile(@"C:\asd\ClassLibrary1.dll");
            Type[] types= assembly.GetTypes();

            foreach(Type type in types)
            {
                MemberInfo[] members= type.GetMembers();
                foreach(MemberInfo member in members)
                {
                    Console.WriteLine(member.MemberType+" "+member.Name);

                    if (member.MemberType == MemberTypes.Method)
                    {
                        object[] parameters = null;
                        var parametersAsTypes = (member as MethodInfo).GetParameters();
                        if ((member as MethodInfo).GetParameters().Count() > 0)
                            parameters = new object[(member as MethodInfo).GetParameters().Count()];                       
                        foreach(var par in parametersAsTypes)
                        {
                            if (par.ParameterType == typeof(int))
                            {
                                parameters[0] = 1;
                            }
                            else if(par.ParameterType == typeof(string))
                            {
                                parameters[0] = "asdasd";
                            }
                            else
                            {
                                parameters[0] = true;
                            }
                        }
                       var result= (member as MethodInfo).Invoke(Activator.CreateInstance(type), parameters);
                        Console.WriteLine(result);
                    }                    
                }
                MethodInfo method = type.GetMethod("CallMe");
            }
            Console.ReadLine();
        }
    }
}
