export const SetSessionId = (payload: string) => {
  return {
    type: "SET_SESSION_ID",
    payload: payload,
  };
};

export const SetName = (payload: string) => {
  return {
    type: "SET_NAME",
    payload: payload,
  };
};
