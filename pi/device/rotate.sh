#! /bin/sh
### BEGIN INIT INFO
# Provides: rotate
# Required-Start: $syslog
# Required-Stop: $syslog
# Default-Start: 2 3 4 5
# Default-Stop: 0 1 6
# Short-Description: rotate screen
# Description:
### END INIT INFO
 
case "$1" in
    start)
        echo "rotating screen"
        # Starting Programm
		echo 2 | tee /sys/class/graphics/fbcon/rotate_all        
        ;;
    stop)       
        ;;
    *)
        echo "Use: /etc/init.d/rotate {start}"
        exit 1
        ;;
esac
 
exit 0