import { useState, useRef, useEffect } from "react";
import "./style/app.scss";
import Localization from "./service/Localization";

function App() {
  const [okTxt, setOkTxt] = useState("OK");
  const [cancelTxt, setCancelTxt] = useState("Cancel");
  const [lang, setLang] = useState("en");

  useEffect(() => {
    async function loadDataAsync() {
      let btntxt = "";
      let canceltxt = "";
      try {
        btntxt = await Localization(`${lang}/Common_OKButtonText`);
        canceltxt = await Localization(`${lang}/Common_CancelButtonText`);
      } catch (e) {
        console.warn(e);
      } finally {
        setOkTxt(btntxt);
        setCancelTxt(canceltxt);
      }
    }
    loadDataAsync();
  }, [lang]);

  const changeName = (val) => {
    setLang(`${val}`);
  };

  return (
    <div>
      <div className="jumbotron">
        <button onClick={() => changeName("en")}>English</button>
        <button onClick={() => changeName("fi")}>Finnish</button>
      </div>
      <div className="container">
        <button>{okTxt}</button>
        <button>{cancelTxt}</button>
      </div>
    </div>
  );
}

export default App;
