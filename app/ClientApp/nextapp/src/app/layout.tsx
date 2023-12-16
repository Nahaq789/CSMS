import type {Metadata} from "next";
import {Inter} from "next/font/google";
// import "./globals.css";
import Sidebar from "@/components/navigation/Sidebar";
import React from "react";
import {NextFont} from "next/dist/compiled/@next/font";
import SearchBar from "@/components/searchbar/SearchBar";
import TopBar from "@/components/topbar/TopBar";
import styles from '../components/dashboard.module.css'

const inter: NextFont = Inter({subsets: ["latin"]});

export const metadata: Metadata = {
    title: "Create Next App",
    description: "Generated by create next app",
};

export default function RootLayout({
                                       children,
                                   }: {
    children: React.ReactNode;
}) {
    return (
        <html lang="en" className={inter.className}>
        <body>
        <div className={styles.container}>
            <div className={styles.menu}>
                <Sidebar/>
            </div>
            <div className={styles.content}>
                <TopBar/>
                {children}
            </div>
        </div>
        </body>
        </html>
    );
}
