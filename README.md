# ECWP-Winch-Data
This program was written to move data between and ECWP winch with a Remote VPN router and firewall to a ship's network. This is accomplised through connecting to a TCP server on the winch controller. The winch controller then streams data through the firwall to the program. Further, this program can log the raw data, log maximum data (tension and payout),  plot the realtime tension, and transmit UDP packets of data.

The current configuration can read MTNW Legacy, MTNW 1, and the UNOLS Wire String.

To Do:
- [ ] Add cast processing
- [ ] Display Tension Member Alarms and Warning
- [x] ~~Record the UNOLS Winch string~~
- [ ] Display UNOLS Winch string statuses
