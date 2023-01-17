# ECWP-Winch-Data
This program was written to move data between and ECWP winch with a Remote VPN router and firewall to a ship's network. This is accomplised through connecting to a TCP server on the winch controller. The winch controller then streams data through the firwall to the program. Further, this program can log the raw data, log maximum data (tension and payout),  plot the realtime tension, and transmit UDP packets of data.

The current configuration can read MTNW Legacy, MTNW 1, and the UNOLS Wire String.

To Do:
- [ ] Add cast processing
- [ ] Display Tension Member Alarms and Warning
- [ ] Display UNOLS Winch string statuses
- [ ] Add LCI-90i data reception
- [ ] Add Hawboldt Data reception
- [ ] Add in detection and themeing for darkmode
- [i] ~~Add Serial Data Output~~
- [i] ~~Move to Avalonia UI for Cross Platform Application~~
- [i] ~~Record the UNOLS Winch string~~
- [c] ~~Move to .Net MAUI for Cross platform application~~  No Linux support

Systems Supported:
ECWP MASH Winches: Moe, Larry, Curly, Shemp

ECWP 0.322 Hawboldt: Gloria
