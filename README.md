# ECWP-Winch-Data
This program was written to move data between and ECWP winch with a Remote VPN router and firewall to a ship's network. This is accomplished through connecting to a TCP server on the winch controller. The winch controller then streams data through the firwall to the program. Further, this program can log the raw data, log maximum data (tension and payout),  plot the realtime tension, and transmit UDP packets of data.

The current configuration can read MTNW Legacy, MTNW 1, the UNOLS Wire String.
The current version can communicate with ECWP winches: Gloria, Jay Jay, Godzilla, Moe, Larry, Curly, and Shemp (0.322 Hawboldt Winch, IOS Clean Winch, Hawboldt SPRE-3464 and ECWP MASH winches). In addition it can receive TCP data from LCI-90i. Hawboldt winches SPRE-2640RS and SPRE-2648RS also work. The Small Hawboldt winches, SPRE-2036S, from the WCWP should also work. It is possible that other MASH and Hawboldt winches will work.

The latest version can process winch log files to get cast information and place the data in a UNOLS Wire Log format. Winch logs that this can process are: All ECWP Winches, RV Atlantis, WinchDAC, WCWP Traction Winch.

To Do:
- [ ] Generate new wire path diagram
- [ ] Display UNOLS Winch string statuses
- [ ] Start casts (and log files) based on payout and tension
- [ ] Add Graph view to data processing
- [ ] Add Mermac Reception (MacArtney)
- [ ] Add Odim Reception
- [ ] Record Hawboldt winch statuses
- [x] ~~Migrate to TopLevel.StorageProvider API from SaveFileDialog (obsolete)~~
- [x] ~~Insert wire path diagram (existing)~~
- [x] ~~Add unit conversion~~
- [x] ~~Add 3PS Data reception~~
- [x] ~~Record Tension Member Alarms and Warnings in Wire Pool Log~~
- [x] ~~UNOLS Wire Pool Excel Format~~
- [x] ~~Add in detection and themeing for darkmode~~
- [x] ~~Add Wire total wire~~
- [x] ~~Add left on drum amount (total-wire out)~~
- [x] ~~Display tension member alarms and warnings~~
- [x] ~~Dynamically resize line parameter text (Add ability to resize plot)~~
- [x] ~~Configurable time span of charts on a per winch basis (10,20,30,45 seconds)~~
- [x] ~~Display tension member alarm and warning levels on trend graph~~
- [x] ~~End casts (and log files) based on payout and tension~~
- [x] ~~Add Hawboldt Data reception (SPRE-2640RS & SPRE-2648RS)~~
- [x] ~~Add Godzilla Reception (SPRE 3464)~~
- [x] ~~Add WCWP Hawboldt Reception~~
- [x] ~~Add Multi-winch configurations~~
- [x] ~~Add unit display selection~~
- [x] ~~Add cast processing~~
- [x] ~~Record the UNOLS Winch string~~
- [x] ~~Add LCI-90i data reception~~
- [ ] ~~Move to .Net MAUI for Cross platform application~~  No Linux support
- [x] ~~Move to Avalonia UI for Cross Platform Application~~

