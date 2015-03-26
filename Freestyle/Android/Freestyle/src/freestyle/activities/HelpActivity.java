package freestyle.activities;

import android.os.Bundle;
import android.text.Html;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.ActionBarActivityAdapter;

public class HelpActivity extends ActionBarActivityAdapter {

	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_help);
        TextView helpTxt = (TextView) findViewById(R.id.help_text);
        helpTxt.setText(Html.fromHtml(this.getString(R.string.general_help)));
    }

}
