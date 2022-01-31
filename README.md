# ECWP-Winch-Data
This program was written to move data between and ECWP winch with a Remote VPN router and firewall to a ship's network. This is accomplised through connecting to a TCP server on the winch controller. The winch controller then streams data through the firwall to the program. Further, this program can log the raw data, log maximum data (tension and payout),  plot the realtime tension, and transmit UDP packets of data.

The current configuration can read MTNW Legacy, MTNW 1, and the UNOLS Wire String.

To Do:
1) Add cast processing
2) Display Tension Member Alarms and Warning
3) Record the UNOLS Winch string
4) Display UNOLS Winch string statuses
