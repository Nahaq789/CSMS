import React, { useState } from "react";
import LinkButton from "./components/link.tsx";

function App() {
    const title: string = 'Hello World!';
    const [num, setNum] = useState(0);

    const increment = () => {
        setNum(num + 1);
        console.log(num);
    }

    return (
        <div>
            <h1>{title}</h1>
            {num}回押しました。
            <LinkButton text="ボタン" link="/test" />
            <button type="button" onClick={increment}>{title}</button>
        </div>
    )
}

export default App