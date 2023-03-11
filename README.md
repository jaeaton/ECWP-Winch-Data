# ECWP-Winch-Data
This program was written to move data between and ECWP winch with a Remote VPN router and firewall to a ship's network. This is accomplised through connecting to a TCP server on the winch controller. The winch controller then streams data through the firwall to the program. Further, this program can log the raw data, log maximum data (tension and payout),  plot the realtime tension, and transmit UDP packets of data.

The current configuration can read MTNW Legacy, MTNW 1, and the UNOLS Wire String.

The current version can communicate with ECWP winches: Gloria, Moe, Larry, Curly, and Shemp (0.322 Hawboldt Winch, and ECWP MASH winches). In addition it can receive TCP data from LCI-90i.

To Do:
- [ ] Display Tension Member Alarms and Warning
- [ ] Display UNOLS Winch string statuses
- [x] ~~Add Multi-winch configurations~~
- [x] ~~Add unit display selection~~
- [ ] Add unit conversion
- [ ] Add Graph view to data processing
- [ ] Add Hawboldt Data reception
- [ ] Add in detection and themeing for darkmode
- [x] ~~Add cast processing~~
- [x] ~~Record the UNOLS Winch string~~
- [x] ~~Add LCI-90i data reception~~
- ~~[ ] Move to .Net MAUI for Cross platform application~~  No Linux support
- [x] ~~Move to Avalonia UI for Cross Platform Application~~

