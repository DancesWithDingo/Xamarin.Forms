﻿using Xamarin.Forms.Internals;
using Xamarin.Forms.CustomAttributes;
using System.Collections.Generic;

#if UITEST
using Xamarin.Forms.Core.UITests;
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls.Issues
{
#if UITEST
	[Category(UITestCategories.SwipeView)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 10366, "[Enhancement] Change SwipeView to Support RTL", PlatformAffected.Android)]
	public class Issue10366 : TestContentPage
	{
		public Issue10366()
		{
			var layout = new StackLayout
			{
				Padding = 12
			};

			var deleteSwipeItem = new SwipeItem { BackgroundColor = Color.Red, Text = "Delete", IconImageSource = "coffee.png" };
			var favouriteSwipeItem = new SwipeItem { BackgroundColor = Color.Orange, Text = "Favourite", IconImageSource = "bell.png" };

			deleteSwipeItem.Invoked += (sender, e) =>
			{
				DisplayAlert("SwipeView", "Delete Invoked", "OK");
			};

			favouriteSwipeItem.Invoked += (sender, e) =>
			{
				DisplayAlert("SwipeView", "Favourite Invoked", "OK");
			};

			var swipeView = new SwipeView
			{
				HeightRequest = 60,
				BackgroundColor = Color.LightGray,
				LeftItems = new SwipeItems(new List<SwipeItem> { deleteSwipeItem })
				{
					Mode = SwipeMode.Reveal
				},
				RightItems = new SwipeItems(new List<SwipeItem> { favouriteSwipeItem })
				{
					Mode = SwipeMode.Reveal
				}
			};

			var content = new Grid
			{
				BackgroundColor = Color.LightGoldenrodYellow
			};

			var info = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Swipe to the Left or Right"
			};

			content.Children.Add(info);

			swipeView.Content = content;

			var flowDirectionLayout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal
			};

			var flowDirectionButton = new Button
			{
				Text = "Change FlowDirection",
				VerticalOptions = LayoutOptions.Center
			};

			var flowDirectionInfo = new Label
			{
				Text = swipeView.FlowDirection.ToString(),
				VerticalOptions = LayoutOptions.Center
			};

			flowDirectionLayout.Children.Add(flowDirectionButton);
			flowDirectionLayout.Children.Add(flowDirectionInfo);

			layout.Children.Add(swipeView);
			layout.Children.Add(flowDirectionLayout);

			Content = layout;

			flowDirectionButton.Clicked += (sender, args) =>
			{
				if(swipeView.FlowDirection == FlowDirection.RightToLeft)
					swipeView.FlowDirection = FlowDirection.LeftToRight;
				else
					swipeView.FlowDirection = FlowDirection.RightToLeft;

				flowDirectionInfo.Text = swipeView.FlowDirection.ToString();
			};
		}

		protected override void Init()
		{
			Device.SetFlags(new List<string> { ExperimentalFlags.SwipeViewExperimental });
		}
	}
}