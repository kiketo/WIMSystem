﻿using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class PersonsCollection : IPersonsCollection
    {
        // The single instance
        private readonly IDictionary<string, IPerson> personsList;
        private static IPersonsCollection instance;

        static PersonsCollection()
        {
            instance = new PersonsCollection();
        }

        private PersonsCollection()
        {
            this.personsList = new Dictionary<string, IPerson>();
        }

        public static IPersonsCollection Instance
        {
            get { return instance; }
        }

        public IDictionary<string, IPerson> Persons
        {
            get
            {
                return new Dictionary<string, IPerson>(personsList);
            }
        }

        public void AddPerson(IPerson newPerson)
        {
            if (this.personsList.ContainsKey(newPerson.PersonName))
            {
                throw new ArgumentException($"{nameof(ITeam)} with that name exist!"); //Стенли: Не трябва ли да е nameof(IPerson)?
            }
            this.personsList.Add(newPerson.PersonName, newPerson);
        }

        public string ShowAllPeople()
        {
            var output = new StringBuilder();

            if (Persons.Count == 0)
            {
                output.AppendLine("There are no people registered yet!");
            }

            else
            {
                output.AppendLine("Registered people:");

                foreach (var person in Persons)
                {
                    output.AppendLine($"-{person.Value.PersonName}");
                }
            }

            return output.ToString();
        }

        public bool Contains(string name)
        {
            return personsList.ContainsKey(name);
        }

        public IPerson this[string index]
        {
            get => this.personsList[index];
            private set
            {
                this.personsList[index] = value;
            }
        }
    }
}
