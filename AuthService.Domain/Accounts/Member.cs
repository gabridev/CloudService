using AuthService.Core.Abstracts;
using AuthService.Core.Helpers;
using AuthService.Core.Interfaces;
using AuthService.Domain.Events;
using AuthService.Domain.Exceptions;
using System;

namespace AuthService.Domain.Accounts
{
    public class Member : Entity, IAggregateRoot
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _encryptedPassword;
        private string _country;
        private string _phoneNumber;
        private string _postCode;

        public string FirstName 
        {
            get { return _firstName; }
            private set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            private set { _lastName = value; }
        }
        public string Email
        {
            get { return _email; }
            private set { _email = value; }
        }
        public string EncryptedPassword
        { 
            get { return _encryptedPassword; }
            private set { _encryptedPassword = value; }
        }
        public string Country
        {
            get { return _country; }
            private set { _country = value; }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            private set { _phoneNumber = value; }
        }
        public string PostCode
        {
            get { return _postCode; }
            private set { _postCode = value; }
        }

        protected Member() { 
            
        }
 
        private Member(Guid id, string firstName,
                         string lastName) 
        {
            base.Id = (id == default(Guid)) ? Guid.NewGuid() : id;
            _firstName = firstName ?? throw new MemberDomainException($"{nameof(firstName)} is required", new ArgumentException(nameof(firstName)), "Required");
            _lastName = lastName ?? throw new MemberDomainException($"{nameof(lastName)} is required", new ArgumentException(nameof(lastName)), "Required");
        }

        public static Member Create(Guid id,
                                    string firstName, 
                                    string lastName, 
                                    string email,
                                    string password,
                                    string country, 
                                    string phoneNumber, 
                                    string postCode)
           
        {
            Member member = new Member(id, firstName, lastName);

            member.SetEmail(email);
            member.SetPassword(password);
            member._phoneNumber = phoneNumber;
            member._postCode = postCode;
            member._country = country;

            return member;
        }

        private void SetEmail(string email)
        {
            _email = email ?? throw new MemberDomainException($"{nameof(email)} is required.", new ArgumentNullException(nameof(email)), "Required");
        }

        private void SetPassword(string password)
        {
            _encryptedPassword = Encryptation.GetMD5HashData(password) ?? throw new MemberDomainException($"{nameof(password)} is required.", new ArgumentNullException(nameof(password)), "Required");
        }

        public void Update(string firstName, string lastName, string country = null, string phoneNumber = null, string postCode = null)
        {
            _firstName = firstName ?? throw new MemberDomainException($"{nameof(firstName)} is required", new ArgumentException(nameof(firstName)), "Required");
            _lastName = lastName ?? throw new MemberDomainException($"{nameof(lastName)} is required", new ArgumentException(nameof(lastName)), "Required");
            _country = country;
            _phoneNumber = phoneNumber;
            _postCode = postCode;
        }

        public void UpdateEmail(string newEmail)
        {
            if (Email?.ToUpper() != newEmail?.ToUpper())
            {
                string oldEmail = this.Email;
                if (!RegexUtilities.IsValidEmail(newEmail))
                    throw new MemberDomainException($"{nameof(newEmail)} is invalid.", new ArgumentException(nameof(newEmail)), "InvalidEmail");

                this.Email = newEmail ?? throw new MemberDomainException($"{nameof(newEmail)} is required.", new ArgumentNullException(nameof(newEmail)), "Required");

                AddDomainEvent(new MemberEmailUpdatedDomainEvent(oldEmail, this));
            }
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (CheckIsValidPassword(oldPassword))
            {
                SetPassword(newPassword);
            }
        }

        public bool CheckIsValidPassword(string password)
        {
            return Encryptation.ValidateMD5HashData(password, EncryptedPassword);
        }
    }
}
