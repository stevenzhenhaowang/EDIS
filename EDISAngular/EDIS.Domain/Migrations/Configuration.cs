namespace EDIS.Domain.Migrations
{
    using EDIS_DOMAIN;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EDIS_DOMAIN.EDISContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
        }

        protected override void Seed(EDIS_DOMAIN.EDISContext context)
        {
            #region creation
            List<AspNetRole> roles = new List<AspNetRole>
            {
                new AspNetRole{Id="426a02dd-ef81-461a-8016-06524c035397",Name="preclient"},
                new AspNetRole{Id="72369fb5-96b2-451a-b023-29912143233c",Name="client"},
                new AspNetRole{Id="95576ed1-4501-447a-b309-253cb687641c",Name="admin"},
                new AspNetRole{Id="d820c0c6-43f2-4dad-9c63-40cf0c2470cf",Name="preadvisor"},
                new AspNetRole{Id="fee140b5-f264-4f15-b7fb-64ed22fc550f",Name="advisor"},
            };


            List<Service> services = new List<Service>{
                new Service{
                    ServiceId=1,
                    ServiceName="Financial Planning"
                },
                new Service{
                    ServiceId=2,
                    ServiceName="Insurances"
                },
                new Service{
                    ServiceId=3,
                    ServiceName="Accounting Services"
                },
                new Service{
                    ServiceId=4,
                    ServiceName="Investment Services"
                },
                new Service{
                    ServiceId=5,
                    ServiceName="Banking Services"
                },
                new Service{
                    ServiceId=6,
                    ServiceName="Business Development Manager"
                },
            };

            List<SubService> subservices = new List<SubService>
            {
                new SubService{SubServiceName="Foreign Exchange Products",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Futures Products",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Wholesale Services",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Initial Public Offers",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Personal Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Residential Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Residential Investment Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Commercial Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Small Business Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Corporate Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Institutional Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Asset Finance",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Margin Lending",ServiceId=5, SubServiceId=1},

                new SubService{SubServiceName="Business to Business Services",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Managed Funds Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Insurance Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Margin Lending Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Equities Portfolio Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Portfolio Platform Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Trading Platform Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Cash Management Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="International Equity Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Fixed Interest Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Structured Products provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Research Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Foreign Exchange Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="Derivatives Provider",ServiceId=6, SubServiceId=1},

                new SubService{SubServiceName="General Financial Planning",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="Retirement Planning",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="Estate Planning",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="Superannuation",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="Self-Manage Super Fund (SMSF)",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="Centre Link",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="Age Care Planning",ServiceId=1, SubServiceId=1},

                new SubService{SubServiceName="General Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Health Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Life Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Income Protection Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Home and Content Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Motor Vehicle Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Professional Liabilities Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Business Insurance",ServiceId=2, SubServiceId=1},

                new SubService{SubServiceName="Auditing Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="General Accounting Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Accounting and Payroll Mgmt",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Book Keeping",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Personal Tax Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Business Tax Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Corporate Advisory Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Insolvency Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Forensic Accounting Services",ServiceId=3, SubServiceId=1},

                new SubService{SubServiceName="Australian Equity",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="International Equity",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Managed Investments",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Exchange Traded Funds",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Hybrid Securities",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Structured Products",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Direct Property Investment",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Fixed Income Investment",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Derivatives Investment",ServiceId=4, SubServiceId=1},

                new SubService{SubServiceName="Cash and Term Deposits",ServiceId=4, SubServiceId=1},
            };
            #endregion

            context.Services.AddOrUpdate(services.ToArray());
            context.SubServices.AddOrUpdate(subservices.ToArray());
            context.AspNetRoles.AddOrUpdate(roles.ToArray());
            context.SaveChanges();
        }
    }
}
