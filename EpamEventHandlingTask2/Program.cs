using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EpamEventHandlingTask2
{
    class Program
    {
        class OfficeEventArgs : EventArgs
        {
            public int Hour{get; set;}
        }

        class Employee
        {
            public string Name { get; set; }            
            public void Greet(Employee emp, int hour)
            {
                
                if (hour < 12)
                {
                    Console.WriteLine("Доброе утро, {0}!  - сказал {1}", emp.Name, this.Name);
                }
                else if (hour >= 12 && hour < 17)
                {
                    Console.WriteLine("Добрый день, {0}!  - сказал {1}", emp.Name, this.Name);
                }
                else
                {
                    Console.WriteLine("Добрый вечер, {0}!  - сказал {1}", emp.Name, this.Name);
                }
            }
            public void Bye(Employee emp)
            {
                string s = String.Format("До свидания, {0}   - сказал {1}", emp.Name, this.Name);
                Console.WriteLine(s);
            }

            public void Incom(int hour)
            {
                if(Incoming != null)
                    Incoming(this, new OfficeEventArgs { Hour = hour });
            }
            public void Leave(int hour)
            {
                if(Leaving != null)
                    Leaving(this, new OfficeEventArgs { Hour = hour });
            }

            public void ClearSubscribers()
            {
                Incoming = null;
                Leaving = null;
            }

            public event EventHandler<OfficeEventArgs> Incoming;
            public event EventHandler<OfficeEventArgs> Leaving;
        }

        class Office
        {
            private List<Employee> employees = new List<Employee>();
            public int Hour { get; set; }

            public void In(Employee employee)
            {
                Console.WriteLine("На работу пришел {0}", employee.Name);
                foreach (var emp in employees)
                {
                    employee.Incoming += (o, e) => emp.Greet((Employee)o, e.Hour);
                    employee.Leaving += (o, e) => emp.Bye((Employee)o);

                    emp.Incoming += (o, e) => employee.Greet((Employee)o, e.Hour);
                    emp.Leaving += (o, e) => employee.Bye((Employee)o);
                }
                employee.Incom(Hour);
                employees.Add(employee);
            }
            public void Out(Employee emp)
            {
                Console.WriteLine("{0} ушел домой", emp.Name);
                emp.Leave(Hour);
                emp.ClearSubscribers();
                employees.Remove(emp);

                // removing emp from subscribers
                foreach (var e in employees)
                {
                    e.ClearSubscribers();
                    foreach (var ee in employees)
                    {
                        if(!object.ReferenceEquals(e, ee))
                            e.Leaving += (o, eh) => ee.Bye((Employee)o);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Employee john = new Employee { Name = "Джон" };
            Employee bill = new Employee { Name = "Билл" };
            Employee hugo = new Employee { Name = "Хьюго" };

            Office o = new Office();
            o.Hour = 10;
            o.In(john);
            o.Hour = 13;
            o.In(bill);
            o.In(hugo);

            o.Out(john);
            o.Out(bill);
            o.Out(hugo);
           
            Console.ReadKey();
        }
    }
}
