using JetBrains.Annotations;
using SpecialNeeds.Cloudata.Entities;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using SpecialNeeds.Cloudata.Data;
using UnityEngine;

namespace SpecialNeeds.Cloudata.ADO
{
    public class UserRow : DataRow
    {
        protected internal UserRow(DataRowBuilder builder) : base(builder) { }

        public int Id
        {
            get => (int) base[$"{nameof(Id)}"];
            set => base["Id"] = value;
        }

        public string ChildName
        {
            get => (string) base[$"{nameof(ChildName)}"];
            set => base[$"{nameof(ChildName)}"] = value;
        }

        public byte ChildGenderId
        {
            get => (byte) base[$"{nameof(ChildGenderId)}"];
            set => base[$"{nameof(ChildGenderId)}"] = value;
        }

        public byte ChildAge
        {
            get => (byte) base[$"{nameof(ChildAge)}"];
            set => base[$"{nameof(ChildAge)}"] = value;
        }

        public string SpectrumDiagnosis
        {
            get => Convert.ToString(base[$"{nameof(SpectrumDiagnosis)}"]);
            set => base[$"{nameof(SpectrumDiagnosis)}"] = value;
        }

        public string ParentPhotoUri
        {
            get => Convert.ToString(base[$"{nameof(ParentPhotoUri)}"]);
            set => base[$"{nameof(ParentPhotoUri)}"] = value;
        }

        public DateTime CreatedAt
        {
            get => (DateTime) base[$"{nameof(CreatedAt)}"];
            set => base[$"{nameof(CreatedAt)}"] = value;
        }

        public User GetUserEntity()
        {
            return new User
            {
                Id = Id,
                ChildName = ChildName,
                ChildGenderId = ChildGenderId,
                ChildAge = ChildAge,
                SpectrumDiagnosis = SpectrumDiagnosis,
                ParentPhotoUri = ParentPhotoUri,
                CreatedAt = CreatedAt
            };
        }

        public void SetFromUser(User user)
        {
            // Id = user.Id;
            ChildName = user.ChildName;
            ChildGenderId = user.ChildGenderId;
            ChildAge = user.ChildAge;
            SpectrumDiagnosis = user.SpectrumDiagnosis;
            ParentPhotoUri = user.ParentPhotoUri;
            CreatedAt = user.CreatedAt;
        }
    }
}