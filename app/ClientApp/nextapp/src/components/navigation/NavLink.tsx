"use client";
import Link from "next/link";
import {usePathname, useSelectedLayoutSegment} from "next/navigation";
import styles from './navlink.module.css'
import React from "react";

type SelectionProps = {
    slug: string;
    icon: React.ReactNode;
    title: string;
    //children: React.ReactNode;
};

// const NavLink = ({slug, children}: SelectionProps) => {
//     const segment = useSelectedLayoutSegment() || "";
//     const isActive = segment === slug;
//
//     return (
//         <Link href={`/${slug}`} className={styles.nav_container}>
//             {children}
//         </Link>
//     );
// };

const NavLink = ({slug, icon, title}: SelectionProps) => {
    const pathname: string = usePathname();
    return (
        <Link href={slug} className={`${styles.container} ${pathname === slug && styles.active}`}>
            {icon}
            {title}
        </Link>
    )
}

export default NavLink;
