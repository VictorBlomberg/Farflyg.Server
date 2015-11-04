using System;

namespace Farflyg.Server.CyclocityClient
{
    partial class CyclocityStationData
    {
        private CyclocityStationData(int @number, string @name, string @address, double @positionLatitude, double @positoinLongitude, bool @banking, bool @bonus, string @status, string @contractName, int @bikeStands, int @availableBikeStands, int @availableBikes, DateTimeOffset @dataUpdated)
        {
            Number = @number;
            Name = @name;
            Address = @address;
            PositionLatitude = @positionLatitude;
            PositoinLongitude = @positoinLongitude;
            Banking = @banking;
            Bonus = @bonus;
            Status = @status;
            ContractName = @contractName;
            BikeStands = @bikeStands;
            AvailableBikeStands = @availableBikeStands;
            AvailableBikes = @availableBikes;
            DataUpdated = @dataUpdated;
        }

        public static CyclocityStationData Create(int @number, string @name, string @address, double @positionLatitude, double @positoinLongitude, bool @banking, bool @bonus, string @status, string @contractName, int @bikeStands, int @availableBikeStands, int @availableBikes, DateTimeOffset @dataUpdated)
        {
            var _instance = new CyclocityStationData(@number, @name, @address, @positionLatitude, @positoinLongitude, @banking, @bonus, @status, @contractName, @bikeStands, @availableBikeStands, @availableBikes, @dataUpdated);
            return _instance;
        }

        public CyclocityStationData WithNumber(int @number)
        {
            var _instance = new CyclocityStationData(@number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithName(string @name)
        {
            var _instance = new CyclocityStationData(Number, @name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithAddress(string @address)
        {
            var _instance = new CyclocityStationData(Number, Name, @address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithPositionLatitude(double @positionLatitude)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, @positionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithPositoinLongitude(double @positoinLongitude)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, @positoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithBanking(bool @banking)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, @banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithBonus(bool @bonus)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, @bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithStatus(string @status)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, @status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithContractName(string @contractName)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, @contractName, BikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithBikeStands(int @bikeStands)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, @bikeStands, AvailableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithAvailableBikeStands(int @availableBikeStands)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, @availableBikeStands, AvailableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithAvailableBikes(int @availableBikes)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, @availableBikes, DataUpdated);
            return _instance;
        }

        public CyclocityStationData WithDataUpdated(DateTimeOffset @dataUpdated)
        {
            var _instance = new CyclocityStationData(Number, Name, Address, PositionLatitude, PositoinLongitude, Banking, Bonus, Status, ContractName, BikeStands, AvailableBikeStands, AvailableBikes, @dataUpdated);
            return _instance;
        }
    }
}