//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;

namespace UfXtract
{
    public class UfFormats
    {

        private UfFormats() { }


        #region "Compound formats"
        //-----------------------------------------------------------------------


        public static UfFormatDescriber HCard()
        {

            // Construct base hCard
            UfFormatDescriber uFormat = BaseHCard();

            // Add first level of agent
            UfFormatDescriber agenthCard = BaseHCard();
            agenthCard.BaseElement.CompoundName = "agent";
            agenthCard.BaseElement.CompoundAttribute = "class";
            agenthCard.BaseElement.ConcatenateValues = false;
            agenthCard.BaseElement.Multiples = true;
            uFormat.BaseElement.Elements.Add(agenthCard.BaseElement);

            // Add second level of agent
            UfFormatDescriber agenthCard2 = BaseHCard();
            agenthCard2.BaseElement.CompoundName = "agent";
            agenthCard2.BaseElement.CompoundAttribute = "class";
            agenthCard2.BaseElement.ConcatenateValues = false;
            agenthCard2.BaseElement.Multiples = true;
            agenthCard.BaseElement.Elements.Add(agenthCard2.BaseElement);

            // FallBack text only
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("agent", false, true, UfElementDescriber.PropertyTypes.Text));

            return uFormat;

        }

        private static UfFormatDescriber BaseHCard()
        {

            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hCard";
            uFormat.Description = "hCard";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;

            uFormat.BaseElement = new UfElementDescriber("vcard", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("fn", false, false, UfElementDescriber.PropertyTypes.Text));

            uFormat.BaseElement.Elements.Add(Name().BaseElement);
            uFormat.BaseElement.Elements.Add(Adr().BaseElement);
            uFormat.BaseElement.Elements.Add(Email().BaseElement);
            uFormat.BaseElement.Elements.Add(Tel().BaseElement);

            UfFormatDescriber cat = Tag();
            cat.BaseElement.CompoundName = "category";
            cat.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(cat.BaseElement);

            UfFormatDescriber org = Org();
            org.BaseElement.Multiples = true;
            uFormat.BaseElement.Elements.Add(org.BaseElement);

            UfFormatDescriber geo = Geo();
            geo.BaseElement.Multiples = false;
            uFormat.BaseElement.Elements.Add(geo.BaseElement);

            //uFormat.BaseElement.Elements.Add(new UfElementDescriber("agent", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("bday", false, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("class", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("key", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("label", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("logo", false, true, UfElementDescriber.PropertyTypes.Image));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("mailer", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("nickname", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("note", false, true, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("photo", false, true, UfElementDescriber.PropertyTypes.Image));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("rev", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("role", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("sort-string", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("sound", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("title", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("tz", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("uid", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("url", false, true, UfElementDescriber.PropertyTypes.Url));

            return uFormat;

        }

        public static UfFormatDescriber HReview()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hreview";
            uFormat.Description = "A review";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("hreview", true, true, UfElementDescriber.PropertyTypes.None);

            UfFormatDescriber item = HCard();
            item.BaseElement.CompoundName = "item";
            item.BaseElement.CompoundAttribute = "class";
            item.BaseElement.Multiples = false;
            uFormat.BaseElement.Elements.Add(item.BaseElement);

            UfFormatDescriber reviewer = HCard();
            reviewer.BaseElement.CompoundName = "reviewer";
            reviewer.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(reviewer.BaseElement);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("dtreviewed", false, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("summary", false, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("description", false, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("type", false, false, "product,business,event,person,place,website,url"));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("rating", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("best", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("worst", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("version", false, false, UfElementDescriber.PropertyTypes.Text));

            uFormat.BaseElement.Elements.Add(License().BaseElement); // license
            uFormat.BaseElement.Elements.Add(Tag().BaseElement); // tag

            UfFormatDescriber bookmark = Bookmark();
            bookmark.BaseElement.CompoundName = "permalink";
            bookmark.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(bookmark.BaseElement);

            return uFormat;

        }

        public static UfFormatDescriber HCalendar()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hcalendar";
            uFormat.Description = "A event";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("vevent", true, true, UfElementDescriber.PropertyTypes.None);

            UfFormatDescriber cat = Tag();
            cat.BaseElement.CompoundName = "category";
            cat.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(cat.BaseElement);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("summary", true, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("description", false, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("class", false, false, UfElementDescriber.PropertyTypes.Text));

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("dtstart", true, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("dtend", false, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("dtstamp", true, false, UfElementDescriber.PropertyTypes.Date));

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("duration", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("status", false, false, UfElementDescriber.PropertyTypes.Text));

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("uid", true, false, UfElementDescriber.PropertyTypes.Uid));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("url", false, false, UfElementDescriber.PropertyTypes.Url));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("last-modified", false, false, UfElementDescriber.PropertyTypes.Url));


            UfFormatDescriber geo = Geo();
            geo.BaseElement.Multiples = false;
            uFormat.BaseElement.Elements.Add(geo.BaseElement);

   
            
            // Location can be Text, hCard, Adr or Geo
            // Looks for the highest resolution frist
            // --------------

            UfFormatDescriber locationhCard = HCard();
            locationhCard.BaseElement.CompoundName = "location";
            locationhCard.BaseElement.CompoundAttribute = "class";
            locationhCard.BaseElement.ConcatenateValues = false;
            uFormat.BaseElement.Elements.Add(locationhCard.BaseElement);

            UfFormatDescriber locationAdr = Adr();
            locationAdr.BaseElement.CompoundName = "location";
            locationAdr.BaseElement.CompoundAttribute = "class";
            locationAdr.BaseElement.ConcatenateValues = false;
            uFormat.BaseElement.Elements.Add(locationAdr.BaseElement);

            UfFormatDescriber locationGeo = Geo();
            locationGeo.BaseElement.CompoundName = "location";
            locationGeo.BaseElement.CompoundAttribute = "class";
            locationGeo.BaseElement.ConcatenateValues = false;
            uFormat.BaseElement.Elements.Add(locationGeo.BaseElement);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("location", false, false, false, UfElementDescriber.PropertyTypes.Text));

            // --------------


            // Optional extensions to spec
            UfFormatDescriber attendee = HCard();
            attendee.BaseElement.CompoundName = "attendee";
            attendee.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(attendee.BaseElement);

            UfFormatDescriber contact = HCard();
            contact.BaseElement.CompoundName = "contact";
            contact.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(contact.BaseElement);

            UfFormatDescriber organizer = HCard();
            organizer.BaseElement.CompoundName = "organizer";
            organizer.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(organizer.BaseElement);

            uFormat.BaseElement.Elements.Add(RRule().BaseElement);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("rdate", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("tzid", false, false, UfElementDescriber.PropertyTypes.Text));

            return uFormat;

        }

        public static UfFormatDescriber HAtom()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hatom";
            uFormat.Description = "A feed";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("hfeed", true, true, UfElementDescriber.PropertyTypes.None);

            UfFormatDescriber item = HAtomItem();
            uFormat.BaseElement.Elements.Add(item.BaseElement);

            uFormat.BaseElement.Elements.Add(Tag().BaseElement);
            //uFormat.BaseElement.Elements.Add(BuildhAtomItem().BaseElement);

            return uFormat;

        }

        public static UfFormatDescriber HAtomItem()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hentry";
            uFormat.Description = "A entry or feed item";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("hentry", true, true, UfElementDescriber.PropertyTypes.None);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("entry-title", true, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("entry-content", false, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("entry-summary", false, false, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("updated", false, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("published", false, false, UfElementDescriber.PropertyTypes.Date));

            UfFormatDescriber author = HCard();
            author.BaseElement.CompoundName = "author";
            author.BaseElement.CompoundAttribute = "class";
            author.BaseElement.Multiples = false;
            uFormat.BaseElement.Elements.Add(author.BaseElement);

            uFormat.BaseElement.Elements.Add(Tag().BaseElement);

            UfFormatDescriber mk = Bookmark();
            mk.BaseElement.Mandatory = true;
            uFormat.BaseElement.Elements.Add(mk.BaseElement);

            return uFormat;
        }

        public static UfFormatDescriber HResume()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hresume";
            uFormat.Description = "A resume";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("hresume", true, true, UfElementDescriber.PropertyTypes.None);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("summary", false, false, UfElementDescriber.PropertyTypes.FormattedText));


            UfFormatDescriber edu = HCalendar();
            edu.BaseElement.CompoundName = "education";
            edu.BaseElement.CompoundAttribute = "class";
            UfFormatDescriber eduhCard = HCard();
            foreach (UfElementDescriber elementDescriber in eduhCard.BaseElement.Elements)
            {
                edu.BaseElement.Elements.Add(elementDescriber);
            }
            uFormat.BaseElement.Elements.Add(edu.BaseElement);


            UfFormatDescriber exp = HCalendar();
            exp.BaseElement.CompoundName = "experience";
            exp.BaseElement.CompoundAttribute = "class";
            UfFormatDescriber exphCard = HCard();
            foreach (UfElementDescriber elementDescriber in exphCard.BaseElement.Elements)
            {
                exp.BaseElement.Elements.Add(elementDescriber);
            }
            uFormat.BaseElement.Elements.Add(exp.BaseElement);


            UfFormatDescriber aff = HCard();
            aff.BaseElement.CompoundName = "affiliation";
            aff.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(aff.BaseElement);

            UfFormatDescriber ski = Tag();
            ski.BaseElement.CompoundName = "skill";
            ski.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(ski.BaseElement);

            UfFormatDescriber con = HCard();
            con.BaseElement.CompoundName = "contact";
            con.BaseElement.CompoundAttribute = "class";
            con.BaseElement.Multiples = false;
            uFormat.BaseElement.Elements.Add(con.BaseElement);

            return uFormat;
        }


        #endregion



        #region "Elemental formats "
        //-----------------------------------------------------------------------

        public static UfFormatDescriber Xfn()
        {

            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "xfn";
            uFormat.Description = "XHTML Friends Network, describes realtionships";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "xfn";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlTextAttribute;
            uFormat.BaseElement = uFElement;

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("met", ""));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("co-worker", ""));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("colleague", ""));

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("muse", ""));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("crush", ""));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("date", ""));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("sweetheart", ""));

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("co-resident", "neighbor"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("neighbor", "co-resident"));

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("child", "parent sibling spouse kin"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("parent", "child sibling spouse kin"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("sibling", "child parent spouse kin"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("spouse", "child parent sibling kin"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("kin", "child parent sibling spouse"));

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("contact", "acquaintance friend"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("acquaintance", "contact friend"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("friend", "contact friend"));

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("me", "contact acquaintance friend met co-worker colleague co-resident neighbor child parent sibling spouse kin muse crush date sweetheart"));

            return uFormat;

        }

        public static UfFormatDescriber NoFollow()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "nofollow";
            uFormat.Description = "Stops search engines following links";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "nofollow";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("nofollow"));
            return uFormat;
        }

        public static UfFormatDescriber License()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "license";
            uFormat.Description = "License";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "license";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("license"));
            return uFormat;
        }

        public static UfFormatDescriber Directory()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "directory";
            uFormat.Description = "directory";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "directory";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("directory"));
            return uFormat;
        }

        public static UfFormatDescriber Home()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "home";
            uFormat.Description = "home";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "home";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("home"));
            return uFormat;
        }

        public static UfFormatDescriber Payment()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "payment";
            uFormat.Description = "payment";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "payment";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("payment"));
            return uFormat;
        }

        public static UfFormatDescriber Enclosure()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "enclosure";
            uFormat.Description = "enclosure";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "enclosure";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("enclosure"));
            return uFormat;
        }

        public static UfFormatDescriber Tag()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "tag";
            uFormat.Description = "Tag";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "tag";
            uFElement.Multiples = true;
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlTextTag;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("tag"));
            return uFormat;
        }

        public static UfFormatDescriber VoteLinks()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "votelinks";
            uFormat.Description = "Vote Links";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;
            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlTextAttribute;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("vote-for", "vote-abstain vote-against"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("vote-against", "vote-abstain vote-for"));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("vote-abstain", "vote-for vote-against"));
            return uFormat;
        }

        public static UfFormatDescriber XFolk()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "xFolk";
            uFormat.Description = "xFolk";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;

            uFormat.BaseElement = new UfElementDescriber("xfolkentry", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("taggedlink", true, false, UfElementDescriber.PropertyTypes.UrlText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("description", false, true, UfElementDescriber.PropertyTypes.Text));

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "tag";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlTextTag;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("tag"));
            uFormat.BaseElement.Elements.Add(uFElement);


            return uFormat;
        }

        public static UfFormatDescriber Geo()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "geo";
            uFormat.Description = "Location constructed of latitude and longitude";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("geo", false, true, UfElementDescriber.PropertyTypes.Text);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("latitude", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("longitude", false, false, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        public static UfFormatDescriber Adr()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "adr";
            uFormat.Description = "Address";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("adr", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("type", false, true, "work,home,pref,postal,dom,intl"));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("post-office-box", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("extended-address", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("street-address", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("locality", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("region", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("postal-code", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("country-name", false, false, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }


        #endregion


        #region "Reusabe patterns"
        //-----------------------------------------------------------------------

        public static UfFormatDescriber Org()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "org";
            uFormat.Description = "Organization";
            uFormat.BaseElement = new UfElementDescriber("org", true, false, UfElementDescriber.PropertyTypes.Text);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("organization-name", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("organization-unit", false, false, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        public static UfFormatDescriber Email()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "email";
            uFormat.Description = "Email address";
            uFormat.BaseElement = new UfElementDescriber("email", false, true, UfElementDescriber.PropertyTypes.Email);
            uFormat.BaseElement.NodeType = UfElementDescriber.StructureTypes.TypeValuePair;
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("type", false, true, "internet,x400,pref"));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("value", false, false, true, UfElementDescriber.PropertyTypes.Email));
            return uFormat;
        }

        public static UfFormatDescriber Tel()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "tel";
            uFormat.Description = "Telephone";
            uFormat.BaseElement = new UfElementDescriber("tel", false, true, UfElementDescriber.PropertyTypes.Text);
            uFormat.BaseElement.NodeType = UfElementDescriber.StructureTypes.TypeValuePair;
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("type", false, true, "voice,home,msg,work,pref,fax,cell,video,pager,bbs,modem,car,isdn,pcs"));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("value", false, false, true, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        public static UfFormatDescriber Name()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "n";
            uFormat.Description = "Name";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("n", false, false, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("type", false, true, "intl,postal,parcel,work,dom,home,pref"));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("honorific-prefix", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("given-name", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("additional-name", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("family-name", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("honorific-suffix", false, true, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        public static UfFormatDescriber Bookmark()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "bookmark";
            uFormat.Description = "Bookmark";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "bookmark";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("bookmark"));
            return uFormat;
        }

        public static UfFormatDescriber RRule()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "rrule";
            uFormat.Description = "Recurring durations and dates";
            uFormat.BaseElement = new UfElementDescriber("rrule", false, false, UfElementDescriber.PropertyTypes.Text);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("freq", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("count", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("interval", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("until", false, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("bysecond", false, false, UfElementDescriber.PropertyTypes.Number));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("byminute", false, false, UfElementDescriber.PropertyTypes.Number));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("bymonthday", false, false, UfElementDescriber.PropertyTypes.Number));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("byyearday", false, false, UfElementDescriber.PropertyTypes.Number));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("byweekno", false, false, UfElementDescriber.PropertyTypes.Number));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("bymonth", false, false, UfElementDescriber.PropertyTypes.Number));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("byday", false, false, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        #endregion


        #region "POSH pattern"
        //-----------------------------------------------------------------------

        public static UfFormatDescriber NextPrevious()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "nextprevious";
            uFormat.Description = "The rel next previous design pattern";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "nextprevious";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlTextAttribute;
            uFormat.BaseElement = uFElement;

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("next", ""));
            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("prev", ""));

            return uFormat;
        }

        public static UfFormatDescriber Me()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "me";
            uFormat.Description = "The finds rel=me without rest of xfn";
            uFormat.Type = UfFormatDescriber.FormatTypes.Elemental;

            UfElementDescriber uFElement = new UfElementDescriber();
            uFElement.Name = "me";
            uFElement.AllowedTags.Add("a");
            uFElement.AllowedTags.Add("link");
            uFElement.Attribute = "rel";
            uFElement.Type = UfElementDescriber.PropertyTypes.UrlText;
            uFormat.BaseElement = uFElement;

            uFElement.AttributeValues.Add(new UfAttributeValueDescriber("me", ""));


            return uFormat;
        }

        public static UfFormatDescriber HCardXFN()
        {

            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "hCard";
            uFormat.Description = "hCard";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;


            uFormat.BaseElement = new UfElementDescriber("vcard", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("fn", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(Name().BaseElement);
            uFormat.BaseElement.Elements.Add(Adr().BaseElement);
            uFormat.BaseElement.Elements.Add(Geo().BaseElement);
            uFormat.BaseElement.Elements.Add(Org().BaseElement);
            uFormat.BaseElement.Elements.Add(Email().BaseElement);
            uFormat.BaseElement.Elements.Add(Tel().BaseElement);

            UfFormatDescriber cat = Tag();
            cat.BaseElement.CompoundName = "category";
            cat.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(cat.BaseElement);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("agent", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("bday", false, false, UfElementDescriber.PropertyTypes.Date));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("class", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("key", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("label", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("logo", false, true, UfElementDescriber.PropertyTypes.Image));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("mailer", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("nickname", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("note", false, true, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("photo", false, true, UfElementDescriber.PropertyTypes.Image));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("rev", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("role", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("sort-string", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("sound", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("title", false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("tz", false, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("uid", false, false, UfElementDescriber.PropertyTypes.Uid));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("url", false, true, UfElementDescriber.PropertyTypes.Url));

            uFormat.BaseElement.Elements.Add(Xfn().BaseElement);

            return uFormat;

        }

        public static UfFormatDescriber TestSuite()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "test-suite";
            uFormat.Description = "The structure of a test suite containing a number of test fixtures";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("test-suite", true, true, UfElementDescriber.PropertyTypes.None);

            uFormat.BaseElement.Elements.Add(Test().BaseElement);

            return uFormat;
        }

        public static UfFormatDescriber TestFixture()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "test-fixture";
            uFormat.Description = "A test fixture";
            uFormat.Type = UfFormatDescriber.FormatTypes.Compound;
            uFormat.BaseElement = new UfElementDescriber("test-fixture", true, true, UfElementDescriber.PropertyTypes.None);

            uFormat.BaseElement.Elements.Add(new UfElementDescriber("summary", true, true, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("description", false, true, UfElementDescriber.PropertyTypes.FormattedText));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("format", true, false, UfElementDescriber.PropertyTypes.Text));

            uFormat.BaseElement.Elements.Add(Output().BaseElement);
            uFormat.BaseElement.Elements.Add(Assert().BaseElement);

            UfFormatDescriber history = HCalendar();
            history.BaseElement.CompoundName = "history";
            history.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(history.BaseElement);

            UfFormatDescriber author = HCard();
            author.BaseElement.CompoundName = "author";
            author.BaseElement.CompoundAttribute = "class";
            uFormat.BaseElement.Elements.Add(author.BaseElement);

            return uFormat;
        }

        public static UfFormatDescriber Assert()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "assert";
            uFormat.Description = "An Assert, part of a test fixture";
            uFormat.BaseElement = new UfElementDescriber("assert", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("test", false, false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("result", false, false, true, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("comment", false, false, true, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        public static UfFormatDescriber Output()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "output";
            uFormat.Description = "The output, part of a test fixture";
            uFormat.BaseElement = new UfElementDescriber("output", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("url", false, false, true, UfElementDescriber.PropertyTypes.Url));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("type", false, false, true, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }

        public static UfFormatDescriber Test()
        {
            UfFormatDescriber uFormat = new UfFormatDescriber();
            uFormat.Name = "test";
            uFormat.Description = "The location of a test fixture";
            uFormat.BaseElement = new UfElementDescriber("test", false, true, UfElementDescriber.PropertyTypes.None);
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("format", true, false, UfElementDescriber.PropertyTypes.Text));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("url", false, false, true, UfElementDescriber.PropertyTypes.Url));
            uFormat.BaseElement.Elements.Add(new UfElementDescriber("description", false, false, true, UfElementDescriber.PropertyTypes.Text));
            return uFormat;
        }


        #endregion


    }
}
