'use client'
import React from "react";
import "../../app/globals.css";
import SearchBar from "../searchbar/SearchBar";
import styles from './topbar.module.css'
import {MdSearch} from "react-icons/md";
import NotificationsIcon from '@mui/icons-material/Notifications';
import {usePathname} from "next/navigation";
import PublicIcon from '@mui/icons-material/Public';
import ChatIcon from '@mui/icons-material/Chat';

const TopBar = () => {
    const pathName: string = usePathname()
    return (
        <div className={styles.container}>
            <div className={styles.title}>DashBoard</div>
            <div className={styles.menu}>
                <div className={styles.search}>
                    <MdSearch/>
                    <SearchBar/>
                </div>
                <div className={styles.icons}>
                    <ChatIcon/>
                    <PublicIcon/>
                    <NotificationsIcon/>
                </div>
            </div>
        </div>
    );
};

export default TopBar;
