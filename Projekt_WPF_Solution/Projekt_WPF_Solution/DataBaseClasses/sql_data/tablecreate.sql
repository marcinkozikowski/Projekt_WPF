--
-- Struktura tabeli dla tabeli `cars`
--

CREATE TABLE `cars` (
  `ID` int(11) NOT NULL,
  `RegPlate` varchar(32) NOT NULL,
  `Maker` varchar(32) NOT NULL,
  `Model` varchar(32) NOT NULL,
  `ManufacturedYear` int(11) NOT NULL,
  `Engine` int(11) NOT NULL,
  `Type` int(11) NOT NULL,
  `BodyType` varchar(32) NOT NULL,
  `FuelConsumption` double NOT NULL,
  `Image` mediumblob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Struktura tabeli dla tabeli `car_bodytype`
--

CREATE TABLE `car_bodytype` (
  `bodytype` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Struktura tabeli dla tabeli `car_brand`
--
CREATE TABLE `car_brand` (
  `brand` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Struktura tabeli dla tabeli `car_type`
--

CREATE TABLE `car_type` (
  `ID` int(11) NOT NULL,
  `Type` varchar(32) NOT NULL,
  `Price` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Struktura tabeli dla tabeli `clients`
--

CREATE TABLE `clients` (
  `ID` int(11) NOT NULL,
  `Pesel` decimal(11,0) NOT NULL,
  `Name` varchar(32) NOT NULL,
  `Surname` varchar(32) NOT NULL,
  `Born` date NOT NULL,
  `IsMale` tinyint(1) NOT NULL,
  `PhoneNumber` int(11) NOT NULL,
  `Address` varchar(32) NOT NULL,
  `City` varchar(32) NOT NULL,
  `Type` varchar(32) NOT NULL,
  `Image` mediumblob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


--
-- Struktura tabeli dla tabeli `rents`
--

CREATE TABLE `rents` (
  `ID` int(11) NOT NULL,
  `CarID` int(11) NOT NULL,
  `ClientID` int(11) NOT NULL,
  `RentStart` date NOT NULL,
  `RentEnd` date NOT NULL,
  `isReturned` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



--
-- Indeksy dla zrzutów tabel
--

--
-- Indexes for table `cars`
--
ALTER TABLE `cars`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `RegPlate` (`RegPlate`),
  ADD KEY `BodyFK` (`Type`);

--
-- Indexes for table `car_bodytype`
--
ALTER TABLE `car_bodytype`
  ADD UNIQUE KEY `bodytype` (`bodytype`);

--
-- Indexes for table `car_brand`
--
ALTER TABLE `car_brand`
  ADD UNIQUE KEY `brand` (`brand`);

--
-- Indexes for table `car_type`
--
ALTER TABLE `car_type`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Type` (`Type`);

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Pesel` (`Pesel`);

--
-- Indexes for table `rents`
--
ALTER TABLE `rents`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `CarFK` (`CarID`),
  ADD KEY `ClientFK` (`ClientID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT dla tabeli `cars`
--
ALTER TABLE `cars`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT dla tabeli `car_type`
--
ALTER TABLE `car_type`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT dla tabeli `clients`
--
ALTER TABLE `clients`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT dla tabeli `rents`
--
ALTER TABLE `rents`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `cars`
--
ALTER TABLE `cars`
  ADD CONSTRAINT `BodyFK` FOREIGN KEY (`Type`) REFERENCES `car_type` (`ID`);

--
-- Ograniczenia dla tabeli `rents`
--
ALTER TABLE `rents`
  ADD CONSTRAINT `CarFK` FOREIGN KEY (`CarID`) REFERENCES `cars` (`ID`),
  ADD CONSTRAINT `ClientFK` FOREIGN KEY (`ClientID`) REFERENCES `clients` (`ID`);