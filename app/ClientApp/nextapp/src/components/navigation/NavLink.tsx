"use client";
import Link from "next/link";
import { useSelectedLayoutSegment } from "next/navigation";

type SelectionProps = {
  slug: string;
  children: React.ReactNode;
};

const NavLink = ({ slug, children }: SelectionProps) => {
  const segment = useSelectedLayoutSegment() || "";
  const isActive = segment === slug;

  return (
    <Link href={`/${slug}`} className={isActive ? "active" : undefined}>
      {children}
    </Link>
  );
};

export default NavLink;
