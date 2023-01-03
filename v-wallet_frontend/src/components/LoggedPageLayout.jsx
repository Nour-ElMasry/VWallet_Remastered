import Header from "./Header";

const LoggedPageLayout = (props) => {
    return <div>
        <Header log />
        {props.children}
    </div>;
}
    

export default LoggedPageLayout;