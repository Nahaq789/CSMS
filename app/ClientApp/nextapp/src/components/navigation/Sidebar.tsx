import NavLink from "./NavLink";
import styles from './sidebar.module.css'
import DashboardIcon from '@mui/icons-material/Dashboard';
import AccessibilityIcon from '@mui/icons-material/Accessibility';
import HandshakeIcon from '@mui/icons-material/Handshake';
import PixIcon from '@mui/icons-material/Pix';
import TaskIcon from '@mui/icons-material/Task';
import ShoppingCartCheckoutIcon from '@mui/icons-material/ShoppingCartCheckout';

const Sidebar = () => {
    return (
        <div className={styles.container}>
            <div className={styles.app_name}>SAKSAK</div>
            <ul className={styles.list}>
                <li>
                    <NavLink slug={"/dashboard"}>
                        <DashboardIcon/>
                        <span className={styles.cat}>Dashboard</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink slug={"/dashboard"}>
                        <AccessibilityIcon/>
                        <span className={styles.cat}>Customer</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink slug={"/dashboard"}>
                        <HandshakeIcon/>
                        <span className={styles.cat}>Contracts</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink slug={"/dashboard"}>
                        <PixIcon/>
                        <span className={styles.cat}>Estimate</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink slug={"/dashboard"}>
                        <TaskIcon/>
                        <span className={styles.cat}>Task</span>
                    </NavLink>
                </li>
                <li>
                    <NavLink slug={"/dashboard"}>
                        <ShoppingCartCheckoutIcon/>
                        <span className={styles.cat}>Order</span>
                    </NavLink>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;
