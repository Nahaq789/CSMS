"use client";
import Link from "next/link";
import {useSelectedLayoutSegment} from "next/navigation";
import styles from './sidebar.module.css'

type SelectionProps = {
    slug: string;
    children: React.ReactNode;
};

const NavLink = ({slug, children}: SelectionProps) => {
    const segment = useSelectedLayoutSegment() || "";
    const isActive = segment === slug;

    return (
        <Link href={`/${slug}`} className={styles.nav_container}>
            {children}
        </Link>
    );
};

export default NavLink;
