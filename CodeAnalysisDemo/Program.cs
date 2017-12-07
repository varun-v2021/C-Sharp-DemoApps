using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysisDemo
{
    class Cart {
        string[] items;

        public Cart() {
            items = new string[3];
            items[0] = "HTC Phone";
            items[1] = "Samsung Phone";
            items[2] = "LG Phone";
        }

        public void DisplayItems() {
            foreach (string item in items)
                Console.WriteLine(item);
        }
    }

    public class Program
    {
        public static void Main()
        {
            Cart shoppingCart = new Cart();
            shoppingCart.DisplayItems();

            string productName = "Samsung Galaxy J7";
            double mobilePrice = 15000.00;
            short quantity = 100;
            double discount = .05;
            // array of objects 
            //Converting value types to reference type (object) is called Boxing and opposite process is Unboxing
            object[] productDetails = { productName, mobilePrice, quantity, discount };
            Purchase purchase1 = new Purchase();
            purchase1.PrintPurchaseDetails(productDetails);


            //Object class is the parent of all types in C#.


            //Unboxing example
            int qty = Convert.ToInt32(productDetails[2]);
            double mobilePrice1 = Convert.ToDouble(productDetails[1]);
            Console.WriteLine(mobilePrice1);

            Program1 prg1 = new Program1();
            prg1.init();

            Program2 prg2 = new Program2();
            prg2.init2();
            prg2.CalculateBill();
            prg2.CalculateBill(12,13);
            prg2.CalculateBill(12.5,13.5,14.5,15,20);

            prg2.Area(5);

            prg2.PrintCourseDetails("MVC","Sharepoint","Azure");


            //TestApplication for Inheritance concept
            {
                // To understand the order of constructor calls
                RegularCustomer customerOne = new RegularCustomer();
                EliteCustomer customerTwo = new EliteCustomer();

                //To print customer details
                RegularCustomer customerThree = new
                        RegularCustomer("Tom", "9876543456", "Tom@gmail.com", "New Delhi", 15);
                Console.WriteLine(customerThree.CustomerId);
                Console.WriteLine(customerThree.CustomerName);
                Console.WriteLine(customerThree.ContactNumber);
                Console.WriteLine(customerThree.EmailId);
                Console.WriteLine(customerThree.Address);
                Console.WriteLine(customerThree.DiscountPercentage);
                EliteCustomer customerFour = new
                       EliteCustomer("Tibi", "9876543218", "Tibi@gmail.com", "New York", 4);
                Console.WriteLine(customerFour.CustomerId);
                Console.WriteLine(customerFour.CustomerName);
                Console.WriteLine(customerFour.ContactNumber);
                Console.WriteLine(customerFour.EmailId);
                Console.WriteLine(customerFour.Address);
                Console.WriteLine(customerFour.CouponsOwned);

                //Overridden method example
                Customer custObj = new RegularCustomer();
                custObj.getCustomerDetails();
                //by using the base class reference, only the members defined in the base class 
                //and only the overridden method(s) in the derived class are accessible
                //custObj.DiscountPercentage();   //cannot access child specific variables using a base reference object
                //Typecasting to solve this
                RegularCustomer regCustObj = (RegularCustomer)custObj;
                //OR
                RegularCustomer regCustObj1 = custObj as RegularCustomer; // "as" keyword to achieve same as above
                Console.WriteLine(regCustObj.DiscountPercentage);


                //Examples Interface implementation
                ITax sellerSalesTax = new Seller("S1001", "Steve",
                "New Delhi");
                Console.WriteLine(sellerSalesTax.PayTax());

                IGovt sellerGovtTax = new Seller("S1002", "Rob",
                "Mysore");
                Console.WriteLine(sellerGovtTax.PayTax());
            }

        }
    }

    public class Purchase
    {

        public string purchaseId;
        public int quantityOrdered;
        private string shippingAddress;
        private string productName;
        public Purchase(string purchaseId, string productName, int quantityOrdered, string shippingAddress)
        {
            this.purchaseId = purchaseId;
            this.productName = productName;
            this.quantityOrdered = quantityOrdered;
            this.shippingAddress = shippingAddress;
        }
        public Purchase()
        {

        }
        public void PrintPurchaseDetails(object[] purchaseDetails)
        {
            Console.WriteLine("Purchase Details are :");
            Console.WriteLine("----------------------------------------------");
            foreach (object item in purchaseDetails)
                Console.WriteLine(item);
            Console.WriteLine("----------------------------------------------");
        }
    }

    class Product
    {
        private string productName;
        public Product()
        {
            Console.WriteLine("Parameterless Constructor");
        }
        public Product(string productName)
            : this()
        {
            this.productName = productName;
            Console.WriteLine("Parameterized Constructor");
        }
        static Product()
        {
            Console.WriteLine("Static Constructor");
        }
    }
    class Program1
    {
        public void init()
        {
            //static constructor is initialised only once even though there are two parameterless constructor calls
            Product product1 = new Product();   
            Product product3 = new Product();
            Product product2 = new Product("cup");
        }        
    }


    public class Student
    {
        static int studentCount = 100;
        int studentId;
        public Student()
        {
            this.studentId = studentCount++;
            Console.WriteLine("Constructor");
        }
        static Student()
        {
            studentCount = 100;
            Console.WriteLine("Static Constructor");
        }
        public static void ShowCount()
        {
            Console.WriteLine("Show count : " + (studentCount - 100) + " ");
        }
    }
    public class Program2
    {

        //static constructor is initialised only once even though there are two parameterless constructor calls
        public void init2()
        {
            Student object1 = new Student();
            Student.ShowCount();
            Student object2 = new Student();
            Student.ShowCount();
        }

        //params allows method to receive variable number of parameters
        //and these parameters passed to a method will be converted into a temporary array. 
        //They are comma-separated list of arguments of the type specified in the parameter declaration. 
        public double CalculateBill(params double[] values)
        {
            double result = 0;
            foreach (double val in values)
            {
                result += val;
            }
            Console.WriteLine("Bill value is: " + result);
            return result;
        }

        public static double Area(int a = 15, int b = 5)
        {
            int area = (a * b);
            return area;
        }
        public double Area(int a = 10)
        {
            double area = (3.14 * a * a);
            return area;
        }

        public void PrintCourseDetails(string mainSubj = "C#", params string[] electives)
        {
            Console.WriteLine("Main sub : {0}", mainSubj);
            Console.WriteLine("No. of electives :{0} ", electives.Count());
        }
    }

    sealed class Purchase1 {
    }

    //Compile time error as sealed typed cannot be inherited
    //class FestivalPurchase : Purchase1 {
    //}


    public class Customer
    {
        private static int count;
        private int customerId;
        private string customerName;
        private string contactNumber;
        private string emailId;
        private string address;
        static Customer()
        {
            count = 1000;
        }
        public Customer()
        {
            CustomerId = ++count;
            Console.WriteLine("base class constructor..");
        }
        public Customer(string name, string contactNumber, string emailid, string
                                                                          address)
        : this()  // invoking default contructor to set auto incremented customer id
        {
            this.CustomerName = name;
            this.ContactNumber = contactNumber;
            this.EmailId = emailid;
            this.Address = address;
        }
        public int CustomerId
        {
            get
            {
                return customerId;
            }
            set
            {
                customerId = value;
            }
        }
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }
        public string ContactNumber
        {
            get
            {
                return contactNumber;
            }
            set
            {
                contactNumber = value;
            }
        }
        public string EmailId
        {
            get
            {
                return emailId;
            }
            set
            {
                emailId = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public virtual void getCustomerDetails() {
            Console.WriteLine("Printing customer details in Parent method");
        }
    }


    public class RegularCustomer : Customer
    {
        private double discountPercentage;
        public RegularCustomer()
        {
            Console.WriteLine("derived class- RegularCustomer constructor..");
        }
        public RegularCustomer(string name, string contactNumber, string emailid,
                                                 string address, double discount)
            : base(name, contactNumber, emailid, address) // here the 'base' keyword is used  to invoke parent class constructor to initialize the inherited members
        {
            this.discountPercentage = discount;
        }
        public double DiscountPercentage
        {
            get
            {
                return discountPercentage;
            }
            set
            {
                discountPercentage = value;
            }
        }

        //It is not mandatory for a derived class to override a virtual method
        //Static method cannot be marked as virtual
        //We cannot override a non-virtual method of a base class
        public override void getCustomerDetails()
        {
            Console.WriteLine("Printing customer details in child RegularCustomer method");
        }
    }

    public class EliteCustomer : Customer
    {
        private int couponsOwned;
        public EliteCustomer()
        {
            Console.WriteLine("derived class- EliteCustomer constructor..");
        }
        public EliteCustomer(string name, string contactNumber, string emailid, string
                                                                 address, int coupons)
            : base(name, contactNumber, emailid, address) // here the 'base' keyword is used to invoke parent class constructor to initialize the inherited members
        {
            this.couponsOwned = coupons;
        }
        public int CouponsOwned
        {
            get
            {
                return couponsOwned;
            }
            set
            {
                couponsOwned = value;
            }
        }
        public double GetDiscount()
        {
            double discount = CouponsOwned * .01;
            this.CouponsOwned = 0;
            return discount;
        }
    }
    
    public class Parent
    {
        private string name;

        //Compilation error is this is not present, also depends whether a no-argument constructor is present in child class
        public Parent() {
        }

        public Parent(string name)
        {
            Console.WriteLine(name + " is in Parent Parameterized constructor");
        }
    }
    public class Child : Parent
    {
        //if constructor is present in child, then corresponding constructor with no arguments must be availble in parent
        public Child()
        {
            Console.WriteLine("Child Parameterless constructor");
        }
        public Child(string name)
            : base(name)
        {
            Console.WriteLine(name + " is in Child Parameterized constructor");
        }
    }

}
