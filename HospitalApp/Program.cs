using System;

namespace HospitalManagementSystem
{
    public delegate double BillingStrategy(double amount);
    public delegate void NotificationHandler(string message);

    abstract class Patient
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Disease { get; set; }
        public string Contact { get; set; }

        public abstract double CalculateBill();
    }

    class InPatient : Patient
    {
        public int Days { get; set; }
        public double ChargePerDay { get; set; }

        public override double CalculateBill()
        {
            return Days * ChargePerDay;
        }
    }

    class OutPatient : Patient
    {
        public double ConsultationFee { get; set; }

        public override double CalculateBill()
        {
            return ConsultationFee;
        }
    }

    class Emergency : Patient
    {
        public double EmergencyCharge { get; set; }
        public double TreatmentCost { get; set; }

        public override double CalculateBill()
        {
            return EmergencyCharge + TreatmentCost;
        }
    }

    class Billing
    {
        public BillingStrategy Strategy;

        public double GenerateBill(double amount)
        {
            return Strategy(amount);
        }
    }

    class Hospital
    {
        public event NotificationHandler PatientAdmitted;

        public void AdmitPatient(Patient p)
        {
            PatientAdmitted?.Invoke(
                $"Patient {p.Name}, Age {p.Age}, Gender {p.Gender}, Disease {p.Disease} admitted"
            );
        }
    }

    class AccountsDepartment
    {
        public void Notify(string msg)
        {
            Console.WriteLine("Accounts: " + msg);
        }
    }

    class AdminDepartment
    {
        public void Notify(string msg)
        {
            Console.WriteLine("Admin: " + msg);
        }
    }

    class Program
    {
        static double NormalBilling(double amount)
        {
            return amount;
        }

        static double InsuranceBilling(double amount)
        {
            return amount * 0.7;
        }

        static void Main()
        {
            Hospital hospital = new Hospital();
            AccountsDepartment accounts = new AccountsDepartment();
            AdminDepartment admin = new AdminDepartment();

            hospital.PatientAdmitted += accounts.Notify;
            hospital.PatientAdmitted += admin.Notify;

            Console.WriteLine("Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Age:");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Gender:");
            string gender = Console.ReadLine();

            Console.WriteLine("Disease:");
            string disease = Console.ReadLine();

            Console.WriteLine("Contact:");
            string contact = Console.ReadLine();

            Console.WriteLine("1-InPatient  2-OutPatient  3-Emergency");
            int type = int.Parse(Console.ReadLine());

            Patient patient;

            if (type == 1)
            {
                patient = new InPatient
                {
                    Name = name,
                    Age = age,
                    Gender = gender,
                    Disease = disease,
                    Contact = contact,
                    Days = 4,
                    ChargePerDay = 2500
                };
            }
            else if (type == 2)
            {
                patient = new OutPatient
                {
                    Name = name,
                    Age = age,
                    Gender = gender,
                    Disease = disease,
                    Contact = contact,
                    ConsultationFee = 1200
                };
            }
            else
            {
                patient = new Emergency
                {
                    Name = name,
                    Age = age,
                    Gender = gender,
                    Disease = disease,
                    Contact = contact,
                    EmergencyCharge = 3000,
                    TreatmentCost = 5000
                };
            }

            hospital.AdmitPatient(patient);

            double baseBill = patient.CalculateBill();

            Billing billing = new Billing();

            Console.WriteLine("Apply Insurance? yes/no");
            string option = Console.ReadLine();

            billing.Strategy = option == "yes"
                               ? InsuranceBilling
                               : NormalBilling;

            double finalBill = billing.GenerateBill(baseBill);

            Console.WriteLine("Final Bill: " + finalBill);
        }
    }
}
