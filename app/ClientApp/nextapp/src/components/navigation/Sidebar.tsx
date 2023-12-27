import NavLink from "./NavLink";
import styles from "./sidebar.module.css";
import DashboardIcon from "@mui/icons-material/Dashboard";
import AccessibilityIcon from "@mui/icons-material/Accessibility";
import HandshakeIcon from "@mui/icons-material/Handshake";
import PixIcon from "@mui/icons-material/Pix";
import TaskIcon from "@mui/icons-material/Task";
import ShoppingCartCheckoutIcon from "@mui/icons-material/ShoppingCartCheckout";
import { ShopOutlined } from "@mui/icons-material";
import { Inter } from "next/font/google";
import WorkIcon from "@mui/icons-material/Work";
import AnalyticsIcon from "@mui/icons-material/Analytics";
import GroupIcon from "@mui/icons-material/Group";
import SettingsIcon from "@mui/icons-material/Settings";
import HelpIcon from "@mui/icons-material/Help";

interface MenuItem {
  title: string;
  list: SubMenuItem[];
}

interface SubMenuItem {
  title: string;
  path: string;
  icon: JSX.Element; // Assuming your icons are React components
}

const menuItem: MenuItem[] = [
  {
    title: "Page",
    list: [
      {
        title: "Dashboard",
        path: "/dashboard",
        icon: <DashboardIcon />,
      },
      {
        title: "Customer",
        path: "/customer",
        icon: <AccessibilityIcon />,
      },
      {
        title: "Contracts",
        path: "/contracts",
        icon: <HandshakeIcon />,
      },
      {
        title: "Estimate",
        path: "/estimate",
        icon: <PixIcon />,
      },
      {
        title: "Task",
        path: "/task",
        icon: <TaskIcon />,
      },
      {
        title: "Order",
        path: "/order",
        icon: <ShoppingCartCheckoutIcon />,
      },
    ],
  },
  {
    title: "Analytics",
    list: [
      {
        title: "Revenue",
        path: "/dashboard/revenue",
        icon: <WorkIcon />,
      },
      {
        title: "Reports",
        path: "/dashboard/reports",
        icon: <AnalyticsIcon />,
      },
      {
        title: "Teams",
        path: "/dashboard/teams",
        icon: <GroupIcon />,
      },
    ],
  },
  {
    title: "User",
    list: [
      {
        title: "Settings",
        path: "/dashboard/settings",
        icon: <SettingsIcon />,
      },
      {
        title: "Help",
        path: "/dashboard/help",
        icon: <HelpIcon />,
      },
    ],
  },
];

const Sidebar = async () => {
  return (
    <div className={styles.container}>
      <div className={styles.app_name}>SAKSAK</div>
      <ul className={styles.list}>
        {menuItem.map((cat: MenuItem) => (
          <li key={cat.title}>
            <span className={styles.cat}>{cat.title}</span>
            {cat.list.map((item: SubMenuItem) => (
              <NavLink
                slug={item.path}
                key={item.title}
                icon={item.icon}
                title={item.title}
              />
            ))}
          </li>
        ))}
      </ul>
    </div>
  );
};
// const Sidebar = () => {
//     return (
//         <div className={styles.container}>
//             <div className={styles.app_name}>SAKSAK</div>
//             <ul className={styles.list}>
//                 <li>
//                     <NavLink slug={"/dashboard"}>
//                         <DashboardIcon/>
//                         <span className={styles.cat}>Dashboard</span>
//                     </NavLink>
//                 </li>
//                 <li>
//                     <NavLink slug={"/dashboard"}>
//                         <AccessibilityIcon/>
//                         <span className={styles.cat}>Customer</span>
//                     </NavLink>
//                 </li>
//                 <li>
//                     <NavLink slug={"/dashboard"}>
//                         <HandshakeIcon/>
//                         <span className={styles.cat}>Contracts</span>
//                     </NavLink>
//                 </li>
//                 <li>
//                     <NavLink slug={"/dashboard"}>
//                         <PixIcon/>
//                         <span className={styles.cat}>Estimate</span>
//                     </NavLink>
//                 </li>
//                 <li>
//                     <NavLink slug={"/dashboard"}>
//                         <TaskIcon/>
//                         <span className={styles.cat}>Task</span>
//                     </NavLink>
//                 </li>
//                 <li>
//                     <NavLink slug={"/dashboard"}>
//                         <ShoppingCartCheckoutIcon/>
//                         <span className={styles.cat}>Order</span>
//                     </NavLink>
//                 </li>
//             </ul>
//         </div>
//     );
// };

export default Sidebar;
