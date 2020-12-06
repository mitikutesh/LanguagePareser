import axios from "axios";

const Localization = async (relativeUrl) => {
  let result = "";
  try {
    const resp = await axios.get(
      `http://localhost:7071/api/translations/${relativeUrl}`
    );
    // console.log(resp.data);
    result = resp.data;
  } catch (err) {
    // Handle Error Here
    console.error(err);
  }
  return result;
};

export default Localization;
