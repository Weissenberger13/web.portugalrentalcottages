using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BootstrapVillas.Models.Mapping;

namespace BootstrapVillas.Models
{
    public partial class PortugalVillasContext : DbContext
    {                                                                               
        static PortugalVillasContext()
        {
            Database.SetInitializer<PortugalVillasContext>(null);
        }

        public PortugalVillasContext()
            : base("Name=PortugalVillasContext")
        {
        }


        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<AirportDestination> AirportDestinations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingAmendment> BookingAmendments { get; set; }
        public DbSet<BookingDocument> BookingDocuments { get; set; }
        public DbSet<BookingExternal> BookingExternals { get; set; }
        public DbSet<BookingExtra> BookingExtras { get; set; }
        public DbSet<BookingExtraAttribute> BookingExtraAttributes { get; set; }
        public DbSet<BookingExtraPackageMapping> BookingExtraPackageMappings { get; set; }
        public DbSet<BookingExtraParticipant> BookingExtraParticipants { get; set; }
        public DbSet<BookingExtraSelection> BookingExtraSelections { get; set; }
        public DbSet<BookingExtraType> BookingExtraTypes { get; set; }
        public DbSet<BookingExtraVendor> BookingExtraVendors { get; set; }
        public DbSet<BookingParentContainer> BookingParentContainers { get; set; }
        public DbSet<BookingParticipant> BookingParticipants { get; set; }
        public DbSet<BookingType> BookingTypes { get; set; }
        public DbSet<BusinessPaymentAccount> BusinessPaymentAccounts { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyExchange> CurrencyExchanges { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerBankDetail> CustomerBankDetails { get; set; }
        public DbSet<CustomerLogin> CustomerLogins { get; set; }
        public DbSet<CustomerPaymentAccount> CustomerPaymentAccounts { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DepositType> DepositTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentField> DocumentFields { get; set; }
        public DbSet<DocumentFieldTemplateMapping> DocumentFieldTemplateMappings { get; set; }
        public DbSet<DocumentFieldType> DocumentFieldTypes { get; set; }
        public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<EntityCommonField> EntityCommonFields { get; set; }
        public DbSet<EntityInstanceDetailValue> EntityInstanceDetailValues { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<EntityTypeDetailField> EntityTypeDetailFields { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCommand> EventCommands { get; set; }
        public DbSet<EventCommandResult> EventCommandResults { get; set; }
        public DbSet<EventSchemeType> EventSchemeTypes { get; set; }
        public DbSet<EventSubType> EventSubTypes { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }
        public DbSet<FlightProvider> FlightProviders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<InvoiceItemType> InvoiceItemTypes { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<NewsletterParticipant> NewsletterParticipants { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PRCInformation> PRCInformations { get; set; }
        public DbSet<ProcessEvent> ProcessEvents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyEntity> PropertyEntities { get; set; }
        public DbSet<PropertyEntityType> PropertyEntityTypes { get; set; }
        public DbSet<PropertyOwner> PropertyOwners { get; set; }
        public DbSet<PropertyOwnerAccount> PropertyOwnerAccounts { get; set; }
        public DbSet<PropertyOwnerRepresentative> PropertyOwnerRepresentatives { get; set; }
        public DbSet<PropertyPricingCommission> PropertyPricingCommissions { get; set; }
        public DbSet<PropertyPricingSeason> PropertyPricingSeasons { get; set; }
        public DbSet<PropertyPricingSeasonalInstance> PropertyPricingSeasonalInstances { get; set; }
        public DbSet<PropertyPricingType> PropertyPricingTypes { get; set; }
        public DbSet<PropertyRegion> PropertyRegions { get; set; }
        public DbSet<PropertySecurityItem> PropertySecurityItems { get; set; }
        public DbSet<PropertySecurityItemType> PropertySecurityItemTypes { get; set; }
        public DbSet<PropertyStaff> PropertyStaffs { get; set; }
        public DbSet<PropertyStaffTaskAssignment> PropertyStaffTaskAssignments { get; set; }
        public DbSet<PropertyStaffType> PropertyStaffTypes { get; set; }
        public DbSet<PropertyTaskType> PropertyTaskTypes { get; set; }
        public DbSet<PropertyTown> PropertyTowns { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyTypeService> PropertyTypeServices { get; set; }
        public DbSet<PropertyTypeServicesChargeInstance> PropertyTypeServicesChargeInstances { get; set; }
        public DbSet<PropertyVacationType> PropertyVacationTypes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<StandardSeason> StandardSeasons { get; set; }
        public DbSet<StoredSession> StoredSessions { get; set; }
        public DbSet<ThirdPartyService> ThirdPartyServices { get; set; }
        public DbSet<Title> Titles { get; set; }

        public DbSet<EmailTemplate> EmailTemplates { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountTransactionMap());
            modelBuilder.Configurations.Add(new AirportDestinationMap());
            modelBuilder.Configurations.Add(new BookingMap());
            modelBuilder.Configurations.Add(new BookingAmendmentMap());
            modelBuilder.Configurations.Add(new BookingDocumentMap());
            modelBuilder.Configurations.Add(new BookingExternalMap());
            modelBuilder.Configurations.Add(new BookingExtraMap());
            modelBuilder.Configurations.Add(new BookingExtraAttributeMap());
            modelBuilder.Configurations.Add(new BookingExtraPackageMappingMap());
            modelBuilder.Configurations.Add(new BookingExtraParticipantMap());
            modelBuilder.Configurations.Add(new BookingExtraSelectionMap());
            modelBuilder.Configurations.Add(new BookingExtraTypeMap());
            modelBuilder.Configurations.Add(new BookingExtraVendorMap());
            modelBuilder.Configurations.Add(new BookingParentContainerMap());
            modelBuilder.Configurations.Add(new BookingParticipantMap());
            modelBuilder.Configurations.Add(new BookingTypeMap());
            modelBuilder.Configurations.Add(new BusinessPaymentAccountMap());
            modelBuilder.Configurations.Add(new CaseMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CommentReplyMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new CurrencyExchangeMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerBankDetailMap());
            modelBuilder.Configurations.Add(new CustomerLoginMap());
            modelBuilder.Configurations.Add(new CustomerPaymentAccountMap());
            modelBuilder.Configurations.Add(new DepositMap());
            modelBuilder.Configurations.Add(new DepositTypeMap());
            modelBuilder.Configurations.Add(new DocumentMap());
            modelBuilder.Configurations.Add(new DocumentFieldMap());
            modelBuilder.Configurations.Add(new DocumentFieldTemplateMappingMap());
            modelBuilder.Configurations.Add(new DocumentFieldTypeMap());
            modelBuilder.Configurations.Add(new DocumentTemplateMap());
            modelBuilder.Configurations.Add(new DocumentTypeMap());
            modelBuilder.Configurations.Add(new EnquiryMap());
            modelBuilder.Configurations.Add(new EntityCommonFieldMap());
            modelBuilder.Configurations.Add(new EntityInstanceDetailValueMap());
            modelBuilder.Configurations.Add(new EntityTypeMap());
            modelBuilder.Configurations.Add(new EntityTypeDetailFieldMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new EventCommandMap());
            modelBuilder.Configurations.Add(new EventCommandResultMap());
            modelBuilder.Configurations.Add(new EventSchemeTypeMap());
            modelBuilder.Configurations.Add(new EventSubTypeMap());
            modelBuilder.Configurations.Add(new EventTypeMap());
            modelBuilder.Configurations.Add(new FieldTypeMap());
            modelBuilder.Configurations.Add(new FlightMap());
            modelBuilder.Configurations.Add(new FlightBookingMap());
            modelBuilder.Configurations.Add(new FlightProviderMap());
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new InvoiceItemMap());
            modelBuilder.Configurations.Add(new InvoiceItemTypeMap());
            modelBuilder.Configurations.Add(new InvoiceTypeMap());
            modelBuilder.Configurations.Add(new NewsletterParticipantMap());
            modelBuilder.Configurations.Add(new PackageMap());
            modelBuilder.Configurations.Add(new PaymentMap());
            modelBuilder.Configurations.Add(new PaymentTypeMap());
            modelBuilder.Configurations.Add(new PRCInformationMap());
            modelBuilder.Configurations.Add(new ProcessEventMap());
            modelBuilder.Configurations.Add(new PropertyMap());
            modelBuilder.Configurations.Add(new PropertyEntityMap());
            modelBuilder.Configurations.Add(new PropertyEntityTypeMap());
            modelBuilder.Configurations.Add(new PropertyOwnerMap());
            modelBuilder.Configurations.Add(new PropertyOwnerAccountMap());
            modelBuilder.Configurations.Add(new PropertyOwnerRepresentativeMap());
            modelBuilder.Configurations.Add(new PropertyPricingCommissionMap());
            modelBuilder.Configurations.Add(new PropertyPricingSeasonMap());
            modelBuilder.Configurations.Add(new PropertyPricingSeasonalInstanceMap());
            modelBuilder.Configurations.Add(new PropertyPricingTypeMap());
            modelBuilder.Configurations.Add(new PropertyRegionMap());
            modelBuilder.Configurations.Add(new PropertySecurityItemMap());
            modelBuilder.Configurations.Add(new PropertySecurityItemTypeMap());
            modelBuilder.Configurations.Add(new PropertyStaffMap());
            modelBuilder.Configurations.Add(new PropertyStaffTaskAssignmentMap());
            modelBuilder.Configurations.Add(new PropertyStaffTypeMap());
            modelBuilder.Configurations.Add(new PropertyTaskTypeMap());
            modelBuilder.Configurations.Add(new PropertyTownMap());
            modelBuilder.Configurations.Add(new PropertyTypeMap());
            modelBuilder.Configurations.Add(new PropertyTypeServiceMap());
            modelBuilder.Configurations.Add(new PropertyTypeServicesChargeInstanceMap());
            modelBuilder.Configurations.Add(new PropertyVacationTypeMap());
            modelBuilder.Configurations.Add(new SeasonMap());
            modelBuilder.Configurations.Add(new StandardSeasonMap());
            modelBuilder.Configurations.Add(new StoredSessionMap());
            modelBuilder.Configurations.Add(new ThirdPartyServiceMap());
            modelBuilder.Configurations.Add(new TitleMap());

        }
        
    }
}

